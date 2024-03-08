using Gym.Validations;
using System.ComponentModel.DataAnnotations;

namespace Gym.Services.DTO;

public class LoginResetPasswordDTO
{
    [EmailAddressAttribute(ErrorMessage = "Invalid e-mail provided")]
    [TokenEmailValidation]
    public string Email { get; set; }
    [Required]
    [StringLength(18, ErrorMessage = "The {0} must be between {2} and {1} characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [StringLength(18, ErrorMessage = "The {0} must be between {2} and {1} characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}
