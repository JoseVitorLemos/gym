using System.ComponentModel;

namespace Clean.Arch.Domain.Enums;

public enum Roles
{
    [Description("User administrator")]
    Admin,
    [Description("Waiting email of confirmation")]
    EmailConfirmation,
    [Description("User authenticated")]
    Authenticated,
    [Description("Personal trainer")]
    Personal,
    [Description("Fitness Client")]
    FitnessClient
}
