using System.ComponentModel;

namespace Clean.Arch.Helpers.Enums;

public enum HttpMessages
{
    [Description("Created value with success")]
    Post,
    [Description("Get value with success")]
    Get,
    [Description("Update value with success")]
    Update,
    [Description("Delete value with success")]
    Delete,
}
