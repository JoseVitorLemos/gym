using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gym.Validations
{
    public class TokenEmailValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = value as string;

            var httpContextAccessor = validationContext.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            ClaimsPrincipal user = httpContextAccessor?.HttpContext.User;

            string tokenEmail = user?.Claims.FirstOrDefault(x => x.ValueType == "Email")?.Value;
            if (email == tokenEmail)
                return ValidationResult.Success;

            return new ValidationResult("Invalid email authentication provided");
        }
    }
}
