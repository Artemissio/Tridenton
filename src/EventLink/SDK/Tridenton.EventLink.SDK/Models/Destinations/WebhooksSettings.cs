using System.ComponentModel.DataAnnotations;

namespace Tridenton.EventLink.SDK.Destinations;

public sealed record WebhooksSettings : IDestinationSettingsMarker
{
    [Required(ErrorMessage = "HTTP method is required")]
    public WebhookHttpMethod HttpMethod { get; init; }

    public WebhooksSettings()
    {
        HttpMethod = WebhookHttpMethod.Get;
    }
}

[JsonConverter(typeof(EnumerationJsonConverter<WebhookHttpMethod>))]
public sealed class WebhookHttpMethod : Enumeration
{
    private WebhookHttpMethod(int index, string value) : base(index, value) { }

    public static readonly WebhookHttpMethod Get = new(1, "GET");
    public static readonly WebhookHttpMethod Post = new(2, "POST");
    public static readonly WebhookHttpMethod Put = new(3, "PUT");
    public static readonly WebhookHttpMethod Patch = new(4, "PATCH");
    public static readonly WebhookHttpMethod Delete = new(5, "DELETE");
}

public sealed record WebhooksAuthorizationSettings
{
    
}