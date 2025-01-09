using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK.Sources;

/// <summary>
/// Marker interface for source settings
/// </summary>
public interface ISourceSettingsMarker {}

public sealed record SourceSettings
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Source Type is required")]
    public SourceType Type { get; init; }

    [SourceSettingsValidation(ErrorMessage = "PostgreSQL is required")]
    public PostgreSQLSettings? PostgreSQL { get; init; }
    
    public SourceSettings()
    {
        Type = SourceType.None;
    }
    
    private sealed class SourceSettingsValidationAttribute : ConditionalRequirementAttribute<SourceSettings>
    {
        public SourceSettingsValidationAttribute() : base(ValidateSettings) { }

        private static bool ValidateSettings(SourceSettings settings)
        {
            if (settings.Type == SourceType.None)
            {
                return true;
            }

            if (settings.Type == SourceType.PostgreSQL)
            {
                return settings.PostgreSQL is not null;
            }

            return false;
        }
    }
}