using System.Text.Json.Serialization;

namespace TheTool.Core;

public class SourceConfig
{
    public required List<Source> Sources { get; set; }
    public List<string>? FileExtensionFilter { get; set; }
}

public record Source(string Path, string Tag, bool ListFromSubFolder = false);

public record SourceDirectory(string Path, string Name, string Tag);

public record SourceFile(string Path, string Name);

public class ScanCommand
{
    public required SourceConfig SourceConfig { get; set; }
    public required SourceDirectory SourceDirectory { get; set; }
}

public class Response(string Message);

[JsonSerializable(typeof(ScanCommand))]
[JsonSerializable(typeof(IReadOnlyList<SourceDirectory>))]
[JsonSerializable(typeof(List<SourceFile>))]
[JsonSerializable(typeof(Response))]
public partial class MyContext : JsonSerializerContext { }
