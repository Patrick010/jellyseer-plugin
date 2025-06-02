using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.Jellyseerr.Configuration;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        JellyseerrUrl = string.Empty;
        ApiKey = string.Empty;
        EnableRequestButton = true;
        ShowRequestStatus = true;
        RateLimitDelay = 1000;
    }

    /// <summary>
    /// Gets or sets the Jellyseerr URL.
    /// </summary>
    public string JellyseerrUrl { get; set; }

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the request button.
    /// </summary>
    public bool EnableRequestButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show request status.
    /// </summary>
    public bool ShowRequestStatus { get; set; }

    /// <summary>
    /// Gets or sets the API rate limit delay in milliseconds.
    /// </summary>
    public int RateLimitDelay { get; set; }
} 