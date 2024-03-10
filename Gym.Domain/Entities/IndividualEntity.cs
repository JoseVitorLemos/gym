using Gym.Domain.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;
using Gym.Helpers.Validations;

namespace Gym.Domain.Entities;

public sealed class IndividualEntity : BaseEntity
{
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Genders Gender { get; private set; }
    public Guid LoginId { get; private set; }
    public Login Login { get; private set; }

    public ICollection<Professional> Professional { get; set; }
    public ICollection<Workout> Workout { get; set; }

    public IndividualEntity(string name, string cpf, DateTime birthDate,
            Genders gender, Guid loginId)
    {
        Validations(name, cpf, birthDate, gender, loginId);

        Name = name;
        Cpf = cpf;
        BirthDate = birthDate;
        Gender = gender;
        LoginId = loginId;
    }

    private void Validations(string name, string cpf, DateTime birthDate,
            Genders gender, Guid loginId)
    {
        GlobalException.When(string.IsNullOrEmpty(name), "Name is required.");
        GlobalException.When(!cpf.IsValidCpf(), "Cpf is required.");
        GlobalException.When(DateTime.MinValue == birthDate, "BirthDate is required.");
        GlobalException.When(!EnumValidations.IsValidEnum<Genders>(gender), "Gender is required.");
        GlobalException.When(!loginId.IsValidGuid(), "Invalid LoginId provided");
    }
}
