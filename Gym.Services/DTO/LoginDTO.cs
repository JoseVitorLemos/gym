using System.ComponentModel.DataAnnotations;

namespace Gym.Services.DTO;

public class LoginDTO
{
    public Guid Id { get; private set; }
    [EmailAddressAttribute(ErrorMessage = "Invalid e-mail provided")]   
    public string Email { get; set; }
    [Required]
    [StringLength(18, ErrorMessage = "The {0} must be between {2} and {1} characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
