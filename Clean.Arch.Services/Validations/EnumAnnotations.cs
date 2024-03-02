using System.ComponentModel.DataAnnotations;
using Clean.Arch.Helpers.Validations;

namespace Clean.Arch.Validations;

public class EnumAnnotations<T> : ValidationAttribute where T : struct
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (EnumValidations.IsValidEnum<T>(value))
            return ValidationResult.Success;

        return new ValidationResult($"{typeof(T).Name} is required");
    }
}
