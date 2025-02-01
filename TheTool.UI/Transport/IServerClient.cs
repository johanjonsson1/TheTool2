using TheTool.Core;

namespace TheTool.UI.Transport;

public interface IServerClient
{
    Task<List<SourceDirectory>> Get();
    Task<List<SourceFile>> ScanSource(SourceDirectory sourceDirectory);
    Task RequestFromSource(SourceFile sourceFile);
    Task RefreshSources();
}