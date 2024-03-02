using Clean.Arch.Domain.Enums;
using Clean.Arch.Helpers.Exceptions;
using Clean.Arch.Helpers.Validations;

namespace Clean.Arch.Domain.Entities;

public sealed class IndividualEntity : BaseEntity
{
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Genders Gender { get; private set; }

    public ICollection<Professional> Professional { get; set; }
    public ICollection<Workout> Workout { get; set; }
    public ICollection<User> User { get; set; }

    public IndividualEntity(string name, string cpf, DateTime birthDate, Genders gender)
    {
        Validations(name, cpf, birthDate, gender);

        Name = name;
        Cpf = cpf;
        BirthDate = birthDate;
        Gender = gender;
    }

    private void Validations(string name, string cpf, DateTime birthDate, Genders gender)
    {
        GlobalException.When(string.IsNullOrEmpty(name), "Name is required.");
        GlobalException.When(!cpf.IsValidCpf(), "Cpf is required.");
        GlobalException.When(DateTime.MinValue == birthDate, "BirthDate is required.");
        GlobalException.When(!EnumValidations.IsValidEnum<Genders>(gender), "Gender is required.");
    }
}
