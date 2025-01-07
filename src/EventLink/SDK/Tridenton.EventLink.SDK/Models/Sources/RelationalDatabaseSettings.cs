using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK.Sources;

/// <summary>
/// 
/// </summary>
public abstract record RelationalDatabaseSettings
{
    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "URL is required.")]
    public required string Url { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Port is required.")]
    [Range(minimum: 1, maximum: 65535, ErrorMessage = "Port must range from 1 to 65535.")]
    public int Port { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Database is required.")]
    public required string Database { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="RelationalDatabaseSettings"/>
    /// </summary>
    /// <param name="defaultPort">Default port number</param>
    protected RelationalDatabaseSettings(int defaultPort)
    {
        Port = defaultPort;
    }
}