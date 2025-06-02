using System;
using System.Net.Http;
using System.Threading.Tasks;
using Jellyfin.Plugin.Jellyseerr.Configuration;
using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Plugin.Jellyseerr.Api;

/// <summary>
/// The Jellyseerr controller.
/// </summary>
[ApiController]
[Authorize(Policy = "DefaultAuthorization")]
[Route("Jellyseerr")]
public class JellyseerrController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="JellyseerrController"/> class.
    /// </summary>
    /// <param name="httpClientFactory">Instance of the <see cref="IHttpClientFactory"/> interface.</param>
    public JellyseerrController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Searches for media in Jellyseerr.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="type">The media type (movie/tv).</param>
    /// <returns>The search results.</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Search([FromQuery] string query, [FromQuery] string type)
    {
        var config = Plugin.Instance?.Configuration;
        if (config == null || string.IsNullOrEmpty(config.JellyseerrUrl) || string.IsNullOrEmpty(config.ApiKey))
        {
            return BadRequest("Plugin not configured");
        }

        using var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", config.ApiKey);

        var response = await client.GetAsync($"{config.JellyseerrUrl}/api/v1/search?query={Uri.EscapeDataString(query)}&type={type}");
        
        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Error from Jellyseerr API");
        }

        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }

    /// <summary>
    /// Submits a media request to Jellyseerr.
    /// </summary>
    /// <returns>The request result.</returns>
    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Request()
    {
        var config = Plugin.Instance?.Configuration;
        if (config == null || string.IsNullOrEmpty(config.JellyseerrUrl) || string.IsNullOrEmpty(config.ApiKey))
        {
            return BadRequest("Plugin not configured");
        }

        using var reader = new System.IO.StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        using var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", config.ApiKey);

        var response = await client.PostAsync(
            $"{config.JellyseerrUrl}/api/v1/request",
            new StringContent(body, System.Text.Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Error from Jellyseerr API");
        }

        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
} 