using System.ComponentModel.DataAnnotations;
using Gym.Helpers.Utils;

namespace Gym.Validations;

public class GuidAnnotations : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var guid = new Guid(value?.ToString());

        if (GuidValidations.IsValidGuid(guid))
            return ValidationResult.Success;

        return new ValidationResult("Invalid birthDate.");
    }
}
