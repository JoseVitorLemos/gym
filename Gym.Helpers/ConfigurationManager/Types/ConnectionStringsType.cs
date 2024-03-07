using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;

namespace Gym.Helpers.ConfigurationManager.Types;

public class ConnectionStringsType
{
    public string Name { get; private set; }
    public string DefaultConnectionStrings { get; private set; }

    public ConnectionStringsType(string name, string defaultConnectionStrings)
    {
        Validations(name, defaultConnectionStrings);

        Name = name;
        DefaultConnectionStrings = defaultConnectionStrings;
    }

    private void Validations(string name, string defaultConnectionStrings)
    {
        GlobalException.When(name.IsNullOrWhiteSpace(), 
                "Connection name cannot be null");
        GlobalException.When(defaultConnectionStrings.IsNullOrWhiteSpace(), 
                "DefaultConnectionStrings cannot be null");
    }
}
