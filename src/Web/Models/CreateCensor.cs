
namespace Web.Models;


public record CreateCensorModel(string Name, List<string>? Films);

public record EditCensorModel(string? Name, List<string>? Films, string Id);

public record DeleteFilmFromCensorModel(string FilmdId, string CensorId);