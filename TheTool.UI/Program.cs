using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TheTool.Core;
using TheTool.UI.Logging;
using TheTool.UI.Settings;
using TheTool.UI.Transport;

namespace TheTool.UI;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;
        //var cfg = ServiceProvider.GetService<IConfiguration>();
        //var debug = (cfg as IConfigurationRoot).GetDebugView();
        Application.Run(ServiceProvider.GetRequiredService<Form1>());
    }

    public static IServiceProvider ServiceProvider { get; private set; }
    public static IConfiguration Configuration { get; private set; }

    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<Form1>();
                services.Configure<UILogLevel>(context.Configuration.GetSection("UILogLevel"));
                services.Configure<ServerSettings>(context.Configuration.GetSection("ServerSettings"));
                services.Configure<SourceConfig>(context.Configuration.GetSection(nameof(SourceConfig)));
                services.Configure<ClientSettings>(context.Configuration.GetSection(nameof(ClientSettings)));
                services.AddSingleton<SourceConfig>(sp => sp.GetRequiredService<IOptions<SourceConfig>>().Value);
                services.AddSingleton<ClientSettings>(sp => sp.GetRequiredService<IOptions<ClientSettings>>().Value);

                var mock = context.Configuration.GetSection("Mock").Value;
                if (bool.TryParse(mock, out var mocked) && mocked)
                {
                    services.AddTransient<IServerClient, MockServerClient>();
                }
                else
                {
                    services.AddTransient<IServerClient>(sp => sp.GetRequiredService<ServerClient>());
                    services.AddHttpClient<ServerClient>((sp, cli) =>
                    {
                        var settings = sp.GetRequiredService<IOptions<ServerSettings>>().Value;
                        var cliBaseAddress = new Uri($"http://{settings.Ip}:{settings.Port}");
                        cli.BaseAddress = cliBaseAddress;
                        cli.Timeout = TimeSpan.FromMinutes(10);
                    });
                }
            }).AddLogWindow();
    }
}