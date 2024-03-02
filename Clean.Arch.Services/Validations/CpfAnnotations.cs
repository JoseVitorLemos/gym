using System.ComponentModel.DataAnnotations;
using Clean.Arch.Helpers.Utils;
using Clean.Arch.Helpers.Validations;

namespace Clean.Arch.Validations;

public class CpfAnnotations : ValidationAttribute
{
    protected override ValidationResult IsValid(object cpf, ValidationContext validationContext)
    {
        var prop = validationContext.ObjectType.GetProperty(validationContext.MemberName);
        var oldVal = prop.GetValue(validationContext.ObjectInstance) as string;
        prop.SetValue(validationContext.ObjectInstance, RegexHelpers.StringClean(cpf?.ToString(), numerics: false));

        if (Convert.ToString(cpf).IsValidCpf())
            return ValidationResult.Success;

        return new ValidationResult("Invalid cpf format.");
    }
}
