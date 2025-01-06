namespace Tridenton.EventLink.SDK.Sources;

public sealed record PostgreSQLSettings : RelationalDatabaseSettings
{
    [JsonConstructor]
    public PostgreSQLSettings() : base(5432)
    {
    }
}