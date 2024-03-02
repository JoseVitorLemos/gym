using System.ComponentModel.DataAnnotations;
using Clean.Arch.Domain.Enums;
using Clean.Arch.Validations;

namespace Clean.Arch.Services.DTO;

public class IndividualEntityDTO
{    
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [CpfAnnotations(ErrorMessage = "Cpf is required")]
    public string Cpf { get; set; }
    [BirthDateAnnotations(ErrorMessage = "BirthDate is required")]
    public DateTime BirthDate { get; set; }
    [EnumAnnotations<Genders>(ErrorMessage = "Gender is required")]
    public Genders Gender { get; set; }
}
