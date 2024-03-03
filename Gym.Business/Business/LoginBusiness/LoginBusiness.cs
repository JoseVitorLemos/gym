using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.HashPassword;
using Gym.Infrastructure.Smtp;
using Gym.Business.Utils;

namespace Gym.Business.LoginBusiness;

public class LoginBusiness : ILoginBusiness
{
    private readonly IRepository<Login> _loginRepository;
    private readonly IRepository<LoginConfirmation> _emailConfirmation;
    private readonly IRepository<IndividualEntity> _individualEntity;
    private readonly ISmtpSender _mail;
    private readonly IUnitOfWork _unitOfWork;

    public LoginBusiness(IRepository<Login> loginRepository,
                         IRepository<LoginConfirmation> loginConfirmation,
                         IRepository<IndividualEntity> individualEntit,
                         IUnitOfWork unitOfWork,
                         ISmtpSender mail)
    {
        _loginRepository = loginRepository;
        _emailConfirmation = loginConfirmation;
        _individualEntity = individualEntit;
        _unitOfWork = unitOfWork;
        _mail = mail;
    }

    public async Task<Login> Login(Login entity)
    {
        var login = await FindByEmail(entity.Email);

        bool validPassword =
            BcryptAdapter.IsValidPassword(entity.Password, login.Password);

        if (!validPassword)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Invalid password");

        return login;
    }

    public async Task<Login> Signup(Login entity)
    {
        var login = await _loginRepository
            .FindByCondition(x => x.Email.ToLower() == entity.Email.ToLower());

        if (login.FirstOrDefault() != null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email already registered");

        if (!entity.Role.Equals(Roles.EmailConfirmation))
            throw new GlobalException(HttpStatusCodes.BadRequest, "Error invalid role on created user");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            entity.SetPassword(entity.Password);
            await _loginRepository.Insert(entity);

            int codeConfirmation = RandomHelpers.GenerateRandomNumbers(6);

            var confirmation = new LoginConfirmation(entity.Id, codeConfirmation);
            await _emailConfirmation.Insert(confirmation);

            _mail.MailBody = string.Format("Confirmation Code: {0}", codeConfirmation);
            _mail.Title = "Gym confirmation account";
            _mail.To = entity.Email;
           await _mail.SendEmail();

            await _unitOfWork.CommitAsync();

            return entity;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<bool> ResetPassword(Login entity)
    {
        var login = await FindByEmail(entity.Email);

        bool validPassword =
            BcryptAdapter.IsValidPassword(entity.Password, login.Password);

        login.SetPassword(entity.Password);

        if (!validPassword)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Invalid password");

        return await _loginRepository.Update(login);
    }

    private async Task<Login> FindByEmail(string email)
    {
        string emailLower = email.ToLower();

        var login = await _loginRepository
            .FindByCondition(x => x.Email.ToLower() == emailLower);

        if (login.FirstOrDefault() is null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email not registered");

        return login.First();
    }

    public async Task<Login> ResendEmailConfirmation(Login entity)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            int code = RandomHelpers.GenerateRandomNumbers(6);

            var login = await FindByEmail(entity.Email);

            var confirmation = new LoginConfirmation(login.Id, code);

            var result = await _emailConfirmation.Insert(confirmation);

            _mail.MailBody = string.Format("Confirmation Code: {0}", code);
            _mail.Title = "Gym confirmation account";
            _mail.To = entity.Email;

            await _mail.SendEmail();

            await _unitOfWork.CommitAsync();

            return login;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
    }
}