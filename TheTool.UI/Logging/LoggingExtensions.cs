using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TheTool.UI.Logging;

public static class LoggingExtensions
{
    public static IHostBuilder AddLogWindow(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging(builder =>
        {
            builder.ClearProviders();
            builder.Services.AddSingleton(sp => new UILoggerOptions
            {
                LogWindow = () => sp.GetRequiredService<Form1>().LogWindow
            });
            builder.Services.AddSingleton<ILoggerProvider, UILoggerProvider>();
        });

        return hostBuilder;
    }
}
