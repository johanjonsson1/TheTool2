using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TheTool.Core;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace TheTool.UI.Transport;

public class ServerClient : IServerClient
{
    private readonly HttpClient _client;
    private readonly ILogger<ServerClient> _logger;
    private readonly SourceConfig _sourceConfig;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public ServerClient(HttpClient client, ILogger<ServerClient> logger, SourceConfig sourceConfig)
    {
        _client = client;
        _logger = logger;
        _sourceConfig = sourceConfig;
    }

    public async Task<List<SourceDirectory>> Get()
    {
        try
        {
            List<SourceDirectory>? messages = await _client.GetFromJsonAsync<List<SourceDirectory>>(
                $"get",
                JsonSerializerOptions);

            return messages ?? new List<SourceDirectory>();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
        }

        return new List<SourceDirectory>();
    }

    public async Task<List<SourceFile>> ScanSource(SourceDirectory sourceDirectory)
    {
        try
        {
            var scanCommand = new ScanCommand
            {
                SourceConfig = _sourceConfig,
                SourceDirectory = sourceDirectory
            };

            using StringContent json = new(
                JsonSerializer.Serialize(scanCommand, JsonSerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using HttpResponseMessage httpResponse =
                await _client.PostAsync($"scan", json);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<List<SourceFile>>();
        }
        catch (Exception ex)
        {
            _logger.LogError("Scan failed", ex);
        }

        return new List<SourceFile>();
    }

    public async Task RequestFromSource(SourceFile sourceFile)
    {
        try
        {
            using StringContent json = new(
                JsonSerializer.Serialize(sourceFile, JsonSerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using HttpResponseMessage httpResponse =
                await _client.PostAsync($"request", json);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Request succeeded");
                return;
            }

            var response = await httpResponse.Content.ReadFromJsonAsync<Response>();
            _logger.LogError("Request failed with response {Response}", response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Request failed", ex);
        }
    }

    public async Task RefreshSources()
    {
        try
        {
            using StringContent json = new(
                JsonSerializer.Serialize(_sourceConfig, JsonSerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using HttpResponseMessage httpResponse =
                await _client.PutAsync($"refresh", json);

            httpResponse.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
        }
    }
}
