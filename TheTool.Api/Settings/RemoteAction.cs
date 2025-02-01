namespace TheTool.Api.Settings;
public record RemoteAction(string CommandPath, string CommandArgsTemplate, int TimeoutInSeconds);