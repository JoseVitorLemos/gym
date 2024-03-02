using FluentAssertions;
using Clean.Arch.Domain.Entities;
using Clean.Arch.Domain.Enums;
using Clean.Arch.Helpers.Exceptions;

namespace Clean.Arch.Tests;

public class IndividualEntityTest
{
    private readonly IndividualEntity mockIndivualEntity;

    public IndividualEntityTest()
    {
        mockIndivualEntity = MockIndivualEntity();
    }

    private IndividualEntity MockIndivualEntity()
        => new IndividualEntity("Any Name", "613.261.260-24", 
                DateTime.UtcNow, Genders.Male);

    [Fact]
    public void CreatePerson_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new IndividualEntity(mockIndivualEntity.Name, mockIndivualEntity.Cpf, mockIndivualEntity.BirthDate, mockIndivualEntity.Gender);
        action.Should()
              .NotThrow<GlobalException>();
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidName()
    {
        Action action = () => new IndividualEntity(string.Empty, mockIndivualEntity.Cpf, mockIndivualEntity.BirthDate, mockIndivualEntity.Gender);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Name is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidCpf()
    {
        Action action = () => new IndividualEntity(mockIndivualEntity.Name, "123456789", mockIndivualEntity.BirthDate, mockIndivualEntity.Gender);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Cpf is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidBirthDate()
    {
        Action action = () => new IndividualEntity(mockIndivualEntity.Name, mockIndivualEntity.Cpf, DateTime.MinValue, mockIndivualEntity.Gender);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("BirthDate is required.");
    }

    [Fact]
    public void CreatePerson_WithInvalidValidParameters_InvalidGender()
    {
        Enum.TryParse("-1", true, out Genders invalidGender);
        Action action = () => new IndividualEntity(mockIndivualEntity.Name, mockIndivualEntity.Cpf, mockIndivualEntity.BirthDate, invalidGender);
        action.Should().ThrowExactly<GlobalException>()
              .WithMessage("Gender is required.");
    }
}
