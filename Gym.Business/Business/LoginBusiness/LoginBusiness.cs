using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.HashPassword;
using Gym.Infrastructure.Smtp;
using Gym.Business.Utils;
using Gym.Helpers.Utils;

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
            .FindByCondition(x => x.Email == entity.Email);

        if (login.FirstOrDefault() != null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email already registered");

        if (!entity.Role.Equals(Roles.EmailConfirmation))
            throw new GlobalException(HttpStatusCodes.BadRequest, "Error invalid role on created user");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            entity.SetPassword(entity.Password);
            entity.SetStatus(false);
            await _loginRepository.Insert(entity);

            string codeConfirmation = 
                RandomHelpers.GenerateRandom(6, characters: false);

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

    public async Task<bool> ResetPassword(Login entity, string newPassword)
    {
        var login = await FindByEmail(entity.Email);
        bool validPassword =
            BcryptAdapter.IsValidPassword(password: entity.Password,
                                          hashPassword: login.Password);

        login.SetPassword(newPassword);

        if (!validPassword)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Invalid password");

        return await _loginRepository.Update(login);
    }

    private async Task<Login> FindByEmail(string email)
    {
        var login = await _loginRepository
            .FindByCondition(x => x.Email == email);

        if (login.FirstOrDefault() is null)
            throw new GlobalException(HttpStatusCodes.BadRequest, "Email not registered");

        return login.First();
    }

    public async Task<bool> ResendEmailConfirmation(string email)
    {
        await ValidateEmailConfirmed(email);

        DateTime nowLassTwoMinutes = DateTime.UtcNow.AddMinutes(-2);
        var existsCode = await _emailConfirmation
            .FindByCondition(x => x.Login.Email.Equals(email) &&
                             x.CreatedAt >= nowLassTwoMinutes);

        int waitMinutes = 2;
        if (existsCode.Any())
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    string.Format("Email already sent, please wait {0}",
                                  existsCode.First().CreatedAt.GetSecondsByDifferenceNow(waitMinutes)));

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            string codeConfirmation = 
                RandomHelpers.GenerateRandom(6, characters: false);

            var login = await FindByEmail(email);

            await _emailConfirmation
                .ExecuteUpdate(x => x.LoginId.Equals(login.Id),
                               x => x.SetProperty(p => p.Status, false));

            var result = await _emailConfirmation
                .Insert(new LoginConfirmation(login.Id, codeConfirmation));

            _mail.MailBody = string.Format("Confirmation Code: {0}", codeConfirmation);
            _mail.Title = "Gym confirmation account";
            _mail.To = email;
            await _mail.SendEmail();

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
    }

    public async Task<bool> ConfirmEmail(string email, string codeConfirmation)
    {      
        await ValidateEmailConfirmed(email);

        DateTime nowLassTwoHours = DateTime.UtcNow.AddHours(-2);
        var query = await _emailConfirmation
            .FindByCondition(x => x.Code.Equals(codeConfirmation) &&
                             x.CreatedAt >= nowLassTwoHours);

        var emailConfirmation = query.OrderByDescending(x => x.CreatedAt).FirstOrDefault() ??
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Invalid Code confirmation provided");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _emailConfirmation
                .ExecuteUpdate(x => x.Login.Email.Equals(email),
                               x => x.SetProperty(p => p.Status, false));

            emailConfirmation.SetEmailConfirmation(true);
            emailConfirmation.SetConfirmedDate();
            bool result = await _emailConfirmation
                .Update(emailConfirmation);

            var login = await FindByEmail(email);
            login.SetRole(Roles.Authenticated);
            await _loginRepository.Update(login);

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message, e.InnerException);
        }
    }

    public async Task ValidateEmailConfirmed(string email)
    {
        var emailConfirmed = await _emailConfirmation
            .FindByCondition(x => x.Login.Email.Equals(email) &&
                             x.EmailConfirmation);

        if (emailConfirmed.Any())
                throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Email already confirmed");
    }
}
