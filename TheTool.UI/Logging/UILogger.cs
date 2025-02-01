using Microsoft.Extensions.Logging;

namespace TheTool.UI.Logging;

public class UILogger : ILogger
{
    private readonly UILoggerProvider _provider;

    public UILogger(UILoggerProvider provider)
    {
        _provider = provider;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None &&
            _provider.LogLevel != LogLevel.None &&
            _provider.LogLevel <= logLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var logRecord = string.Format("{0} [{1}] {2} {3}\r\n", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

        if (_provider.Options.LogWindow != null)
        {
            var ctl = _provider.Options.LogWindow().logTextBox;
            ctl.Invoke((MethodInvoker)delegate
            {
                ctl.AppendText(logRecord); // UI
            });
        }
    }
}
