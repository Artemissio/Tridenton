namespace Tridenton.EventLink.SDK.Sources;

/// <summary>
/// 
/// </summary>
public sealed record PostgreSQLSettings : RelationalDatabaseSettings
{
    [JsonConstructor]
    public PostgreSQLSettings() : base(5432)
    {
        Schema = "public";
    }
}