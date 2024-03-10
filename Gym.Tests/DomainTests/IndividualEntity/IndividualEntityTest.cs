using FluentAssertions;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Tests;

public class IndividualEntityTest
{
    private readonly IndividualEntity mock;

    public IndividualEntityTest()
    {
        mock = MockIndivualEntity();
    }

    private IndividualEntity MockIndivualEntity()
        => new IndividualEntity("Any Name", "613.261.260-24",
                DateTime.UtcNow, Genders.Male, Guid.NewGuid());

    [Fact]
    public void CreatePerson_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new IndividualEntity(mock.Name, mock.Cpf,
                mock.BirthDate, mock.Gender, mock.LoginId);
        action.Should()
              .NotThrow<GlobalException>();
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidName()
    {
        Action action = () => new IndividualEntity(string.Empty, mock.Cpf,
                mock.BirthDate, mock.Gender, mock.LoginId);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Name is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidCpf()
    {
        Action action = () => new IndividualEntity(mock.Name, "123456789",
                mock.BirthDate, mock.Gender, mock.LoginId);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Cpf is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidBirthDate()
    {
        Action action = () => new IndividualEntity(mock.Name, mock.Cpf,
               DateTime.MinValue, mock.Gender, mock.LoginId);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("BirthDate is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidGender()
    {
        Enum.TryParse("-1", true, out Genders invalidGender);
        Action action = () => new IndividualEntity(mock.Name, mock.Cpf,
                mock.BirthDate, invalidGender, mock.LoginId);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Gender is required.");
    }
}
