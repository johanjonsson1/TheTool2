using System.Diagnostics;
using TheTool.Api;
using TheTool.Api.Settings;
using TheTool.Core;

public class FilesHandler
{
    private readonly ILogger<FilesHandler> _logger;

    public FilesHandler(ILogger<FilesHandler> logger, Cache cache)
    {
        _logger = logger;
    }

    public Task<List<SourceFile>> ScanForSourceFiles(SourceConfig sourceConfig, SourceDirectory directory)
    {
        var rootDir = new DirectoryInfo(directory.Path);
        var files = sourceConfig.FileExtensionFilter?.Any() == true
            ? GetFilteredSourceFiles(sourceConfig, rootDir)
            : rootDir.EnumerateFiles("*.*", SearchOption.AllDirectories).Select(s => new SourceFile(s.FullName, s.Name)).ToList();

        return Task.FromResult(files);
    }

    private static List<SourceFile> GetFilteredSourceFiles(SourceConfig sourceConfig, DirectoryInfo rootDir)
    {
        var searchValue = string.Join(',', sourceConfig.FileExtensionFilter!).AsSpan();
        var files = new List<SourceFile>();

        foreach (var fileInfo in rootDir.EnumerateFiles("*.*", SearchOption.AllDirectories))
        {
            if (searchValue.Contains(fileInfo.Extension, StringComparison.OrdinalIgnoreCase))
            {
                files.Add(new SourceFile(fileInfo.FullName, fileInfo.Name));
            }
        }

        return files;
    }

    public async Task<IResult> RequestSourceFile(RemoteAction remoteAction, SourceFile sourceFile)
    {
        using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(remoteAction.TimeoutInSeconds));
        var p = new Process();
        p.StartInfo.UseShellExecute = true;

        try
        {
            var externalProcess = new Process();
            externalProcess.StartInfo.FileName = remoteAction.CommandPath;
            externalProcess.StartInfo.Arguments = string.Format(remoteAction.CommandArgsTemplate, sourceFile.Path);
            externalProcess.Start();

            await externalProcess.WaitForExitAsync(timeoutCts.Token);

            if (!externalProcess.HasExited)
            {
                _logger.LogError("Command failed with exit code {ExitCode}", externalProcess.ExitCode);
                return Results.Problem($"Command failed with exit code {externalProcess.ExitCode}");
            }

            return Results.Ok();
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Timeout invoking remote action");
            return Results.BadRequest(new Response("Timeout"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Request failed");
            return Results.UnprocessableEntity(new Response($"Request failed with exception {ex.Message}"));
        }
    }
}