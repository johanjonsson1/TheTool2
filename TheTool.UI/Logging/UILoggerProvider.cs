using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TheTool.UI.Logging;

public class UILoggerProvider : ILoggerProvider
{
    private readonly IOptionsMonitor<UILogLevel> _logLevelMonitor;
    public readonly UILoggerOptions Options;
    public LogLevel LogLevel => _logLevelMonitor.CurrentValue.LogLevel;

    public UILoggerProvider(UILoggerOptions options, IOptionsMonitor<UILogLevel> logLevelMonitor)
    {
        Options = options;
        _logLevelMonitor = logLevelMonitor;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new UILogger(this);
    }

    public void Dispose()
    {

    }
}
