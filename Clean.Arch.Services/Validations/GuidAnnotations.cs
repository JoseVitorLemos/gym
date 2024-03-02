using System.ComponentModel.DataAnnotations;
using Clean.Arch.Helpers.Utils;

namespace Clean.Arch.Validations;

public class GuidAnnotations : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var guid = new Guid(value?.ToString());

        if (GuidValidations.IsValid(guid))
            return ValidationResult.Success;

        return new ValidationResult("Invalid birthDate.");
    }
}
