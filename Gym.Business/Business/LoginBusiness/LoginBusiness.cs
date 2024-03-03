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
                         IRepository<LoginConfirmation> loginValidations,
                         IRepository<IndividualEntity> individualEntit,
                         IUnitOfWork unitOfWork,
                         ISmtpSender mail)
    {
        _loginRepository = loginRepository;
        _emailConfirmation = loginValidations;
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

    public async Task<bool> Signup(Login entity)
    {
        var login = await FindByEmail(entity.Email);

        if (!entity.Role.Equals(Roles.EmailConfirmation))
            throw new GlobalException(HttpStatusCodes.BadRequest, "Error invalid role on created user");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            entity.SetPassword(entity.Password);
            var result = await _loginRepository.Insert(entity);

            _mail.MailBody = "Confirm the email";
            _mail.Title = "Create account on gym";
            _mail.To = entity.Email;
            await _mail.SendEmail();

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception e)
        {
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
        finally
        {
            await _unitOfWork.DisposeAsync();
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

        if (login is null)
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
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
        finally
        {
            await _unitOfWork.DisposeAsync();
        }
    }
}
