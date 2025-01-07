namespace Tridenton.EventLink.SDK.Sources;

/// <summary>
/// 
/// </summary>
public sealed record PostgreSQLSettings : RelationalDatabaseSettings, ISourceSettingsMarker
{
    [JsonConstructor]
    public PostgreSQLSettings() : base(5432)
    {
    }
}