using TheTool.Api;
using TheTool.Api.Settings;
using TheTool.Core;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.ConfigureKestrel(o => o.ListenAnyIP(5555));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, MyContext.Default);
});

var remoteAction = builder.Configuration.GetSection(nameof(RemoteAction)).Get<RemoteAction>();
builder.Services.AddSingleton(remoteAction!);
builder.Services.AddSingleton<Cache>();
builder.Services.AddSingleton<PathsHandler>();
builder.Services.AddSingleton<FilesHandler>();

var app = builder.Build();

app.MapGet("/get", async (PathsHandler handler) => await handler.GetAll());
app.MapPut("/refresh", async (PathsHandler handler, SourceConfig config) => await handler.Refresh(config));
app.MapPost("/scan", async (FilesHandler handler, ScanCommand scanCommand) => await handler.ScanForSourceFiles(scanCommand.SourceConfig, scanCommand.SourceDirectory));
app.MapPost("/request", async (FilesHandler handler, RemoteAction action, SourceFile sourceFile) => await handler.RequestSourceFile(action, sourceFile));

app.Run();