using Microsoft.Extensions.Logging;
using TheTool.Core;

namespace TheTool.UI.Transport;

public class MockServerClient : IServerClient
{
    private readonly SourceConfig _sourceConfig;
    private readonly ILogger<MockServerClient> _logger;

    public MockServerClient(SourceConfig sourceConfig, ILogger<MockServerClient> logger)
    {
        _sourceConfig = sourceConfig;
        _logger = logger;
    }

    public async Task<List<SourceDirectory>> Get()
    {
        await Task.Delay(1500);

        return new List<SourceDirectory>
        {
            new SourceDirectory("O:\\Temp\\Mock\\Videos\\Vid 1", "Vid 1", "Videos"),
            new SourceDirectory("O:\\Temp\\Mock\\Vids\\Video 2", "Video 2", "Videos"),

            new SourceDirectory("O:\\Temp\\Mock\\Videos\\Video3", "Video 3", "Videos"),
            new SourceDirectory("X:\\Temp\\Mock\\Photos\\Vacation 1", "Vacation 1", "Images"),
            new SourceDirectory("X:\\Temp\\Mock\\Photos\\Party 2", "Party 2", "Images"),
            new SourceDirectory("Z:\\Mock\\Sound\\Sound 9", "Sound 9", "Audio")
        };
    }

    public async Task RefreshSources()
    {
        await Task.Delay(4000);
    }

    public async Task<List<SourceFile>> ScanSource(SourceDirectory sourceDirectory)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));

        var all = new List<SourceFile>
        {
            new SourceFile($"{sourceDirectory.Path}\\subdir\\file.mp4", "file.mp4"),
            new SourceFile($"{sourceDirectory.Path}\\subdir\\file.jpg", "file.jpg"),
            new SourceFile($"{sourceDirectory.Path}\\file.wav", "file.wav"),
            new SourceFile($"{sourceDirectory.Path}\\file2.nope", "file2.nope"),
            new SourceFile($"{sourceDirectory.Path}\\file3.nope", "file3.nope")
        };

        if (_sourceConfig.FileExtensionFilter?.Any() == true)
        {
            return all.Where(x =>
                    _sourceConfig.FileExtensionFilter.Any(e => x.Path.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(o => o.Path).ToList();
        }

        return all.OrderBy(o => o.Path).ToList();
    }

    public async Task RequestFromSource(SourceFile sourceFile)
    {
        await Task.Delay(TimeSpan.FromSeconds(6));

        if (Random.Shared.Next(0, 10) >= 6)
        {
            _logger.LogError($"Error when requesting {sourceFile.Path}");
            return;
        }

        _logger.LogInformation($"Success when requesting {sourceFile.Path}");
    }
}