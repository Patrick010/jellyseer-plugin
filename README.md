# Jellyfin Jellyseerr Plugin

A Jellyfin plugin that integrates Jellyseerr functionality directly into the Jellyfin web interface.

## Features

- Seamless integration with Jellyseerr
- Request movies and TV shows directly from Jellyfin
- Configure Jellyseerr connection settings
- Rate limiting support
- Request status tracking

## Build & Installation

### Prerequisites

- .NET 6.0 SDK
- Jellyfin server

### Building on Linux/Debian

1. Clone the repository:
```bash
git clone https://github.com/Patrick010/jellyseer-plugin.git
cd jellyseer-plugin
```

2. Build the plugin:
```bash
dotnet build --configuration Release
```

3. The plugin will be created in `Jellyfin.Plugin.Jellyseerr/bin/Release/net6.0/`

### Installation

1. Stop your Jellyfin server
2. Copy the built plugin DLL and its dependencies to your Jellyfin plugins directory:
   - Linux: `/usr/share/jellyfin/plugins` or `/var/lib/jellyfin/plugins`
   - Windows: `C:\ProgramData\Jellyfin\Server\plugins`
3. Start Jellyfin server
4. Go to Dashboard -> Plugins -> Jellyseerr
5. Configure your Jellyseerr URL and API key

## Configuration

1. **Jellyseerr URL**: The full URL to your Jellyseerr instance (e.g., `http://localhost:5055`)
2. **API Key**: Your Jellyseerr API key
3. **Enable Request Button**: Show/hide the request button in the UI
4. **Show Request Status**: Enable/disable request status display
5. **Rate Limit Delay**: Delay between API requests in milliseconds

## Development

The plugin consists of two main parts:
1. C# backend (Jellyfin plugin)
2. Web interface integration

### Project Structure

```
Jellyfin.Plugin.Jellyseerr/
├── Api/                    # API Controllers
├── Configuration/         # Plugin configuration
├── Web/                  # Web interface files
└── Plugin.cs             # Main plugin class
```

## License

This project is licensed under the MIT License. 