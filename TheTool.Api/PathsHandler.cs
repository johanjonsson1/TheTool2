using TheTool.Api;
using TheTool.Core;

public class PathsHandler
{
    private readonly ILogger<PathsHandler> _logger;
    private readonly Cache _cache;

    public PathsHandler(ILogger<PathsHandler> logger, Cache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public async Task<IReadOnlyList<SourceDirectory>> GetAll()
    {
        return await _cache.Get();
    }

    public async Task Refresh(SourceConfig sourceConfig)
    {
        await _cache.Refresh(sourceConfig);
    }
}