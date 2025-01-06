using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK.Sources;

public abstract record SourceSettings
{
    
}

public abstract record RelationalDatabaseSettings
{
    [Required(ErrorMessage = "URL is required.")]
    public required string Url { get; init; }

    [Required(ErrorMessage = "Port is required.")]
    [Range(minimum: 1, maximum: 65535, ErrorMessage = "Port must range from 1 to 65535.")]
    public int Port { get; init; }

    [Required(ErrorMessage = "Database is required.")]
    public required string Database { get; init; }

    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; init; }

    protected RelationalDatabaseSettings(int defaultPort)
    {
        Port = defaultPort;
    }
}