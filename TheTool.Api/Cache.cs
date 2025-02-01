using System.Collections.Concurrent;
using TheTool.Core;

namespace TheTool.Api;

public sealed class Cache
{
    private List<SourceDirectory>? _sourceDirectories;
    private readonly SemaphoreSlim _semaphore = new(1);

    public async Task<IReadOnlyList<SourceDirectory>> Get()
    {
        if (_sourceDirectories == null)
        {
            return new List<SourceDirectory>();
        }

        await Task.CompletedTask;

        return _sourceDirectories!.AsReadOnly();
    }

    public async Task Refresh(SourceConfig sourceConfig)
    {
        var currentState = _sourceDirectories?.Count;

        await _semaphore.WaitAsync();

        try
        {
            if (_sourceDirectories?.Count != currentState)
            {
                return; // someone else has refreshed
            }

            var cd = new ConcurrentDictionary<string, List<SourceDirectory>>();
            Parallel.ForEach(sourceConfig.Sources, (source) =>
            {
                var rootDir = new DirectoryInfo(source.Path);

                var directoryInfos = source.ListFromSubFolder
                    ? rootDir.EnumerateDirectories().SelectMany(s => s.EnumerateDirectories())
                    : rootDir.EnumerateDirectories();

                var sourceDirectoriesForRoot = directoryInfos.Select(s => new SourceDirectory(s.FullName, s.Name, source.Tag)).ToList();

                cd[source.Path] = sourceDirectoriesForRoot;
            });

            var total = cd.Values.SelectMany(x => x).OrderBy(o => o.Name).ToList();
            _sourceDirectories = total;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}