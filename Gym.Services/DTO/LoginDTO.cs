using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using Gym.Domain.Enums;

namespace Gym.Services.DTO;

public class LoginDTO
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public Roles Role { get; set; }
    [EmailAddressAttribute(ErrorMessage = "Invalid e-mail provided")]
    public string Email { get; set; }
    [Required]
    [StringLength(18, ErrorMessage = "The {0} must be between {2} and {1} characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
