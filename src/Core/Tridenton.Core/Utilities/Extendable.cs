using Tridenton.Core.Utilities.Collections;

namespace Tridenton.Core.Utilities;

public abstract record Extendable
{
    public PropertiesCollection Properties { get; init; }

    protected Extendable()
    {
        Properties = new();
    }
}