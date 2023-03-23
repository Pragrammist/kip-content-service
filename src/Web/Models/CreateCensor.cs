
namespace Web.Models;


public record CreateCensorModel(string Name, List<string>? Films);

public record EditCensorModel(string? Name, List<string>? Films);



public record CreateSelectionModel(string Name, List<string>? Films);

public record EditSelectionModel(string? Name, List<string>? Films);
