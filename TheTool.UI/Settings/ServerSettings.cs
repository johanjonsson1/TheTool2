namespace TheTool.UI.Settings
{
    public sealed class ServerSettings
    {
        public required string Ip { get; set; }
        public required int Port { get; set; }
    }

    public sealed class ClientSettings
    {
        public string? ActionCompletedFolder { get; set; }
    }
}
