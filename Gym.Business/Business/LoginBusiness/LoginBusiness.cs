using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.HashPassword;
using Gym.Infrastructure.Smtp;

namespace Gym.Business.LoginBusiness;

public class LoginBusiness : ILoginBusiness
{
    private readonly IRepository<Login> _loginRepository;
    private readonly IRepository<IndividualEntity> _individualEntity;
    private readonly ISmtpSender _mail;
    private readonly IUnitOfWork _unitOfWork;

    public LoginBusiness(IRepository<Login> loginRepository,
                         IRepository<IndividualEntity> individualEntit,
                         IUnitOfWork unitOfWork,
                         ISmtpSender mail)
    {
        _loginRepository = loginRepository;
        _individualEntity = individualEntit;
        _unitOfWork = unitOfWork;
        _mail = mail;
    }

    public async Task<Login> Login(Login entity)
    {
        var login = await FindByEmail(entity.Email);

        if (login is null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email not registered");

        bool validPassword =
            BcryptAdapter.IsValidPassword(entity.Password, login.Password);

        if (!validPassword)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Invalid password");

        return login;
    }

    public async Task<bool> Signup(Login entity)
    {
        var login = await FindByEmail(entity.Email);

        if (login is not null)
            throw new GlobalException(HttpStatusCodes.NotFound, "Email registered");

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

        if (login is null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email not registered");

        bool validPassword =
            BcryptAdapter.IsValidPassword(entity.Password, login.Password);

        login.SetPassword(entity.Password);

        if (!validPassword)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Invalid password");

        return await _loginRepository.Update(login);
    }

    private async Task<Login?> FindByEmail(string email)
    {
        string emailLower = email.ToLower();

        var entity = await _loginRepository
            .FindByCondition(x => x.Email.ToLower() == emailLower);
        return entity.FirstOrDefault();
    }
}
