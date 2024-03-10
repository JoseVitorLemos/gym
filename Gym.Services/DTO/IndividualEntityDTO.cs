using System.ComponentModel.DataAnnotations;
using Gym.Domain.Enums;
using Gym.Validations;

namespace Gym.Services.DTO;

public class IndividualEntityDTO
{    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [CpfAnnotations(ErrorMessage = "Cpf is required")]
    public string Cpf { get; set; }
    [BirthDateAnnotations(ErrorMessage = "BirthDate is required")]
    public DateTime BirthDate { get; set; }
    [EnumAnnotations<Genders>(ErrorMessage = "Gender is required")]
    public Genders Gender { get; set; }
    public Guid LoginId { get; set; }
}
