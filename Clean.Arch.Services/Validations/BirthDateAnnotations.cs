using System.ComponentModel.DataAnnotations;
using Clean.Arch.Helpers.Validations;

namespace Clean.Arch.Validations;

public class BirthDateAnnotations : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime.TryParse(Convert.ToString(value), out DateTime birthDate);

        int age = birthDate.GetAge();

        if (age < 0 || age >= 140)
            return new ValidationResult("Invalid birthDate.");

        return ValidationResult.Success;
    }
}
