using System.ComponentModel.DataAnnotations;

namespace Gym.Services.DTO;

public class LoginDTO
{
    [EmailAddressAttribute(ErrorMessage = "Invalid e-mail provided")]
    public string Email { get; set; }
    [Required]
    [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
