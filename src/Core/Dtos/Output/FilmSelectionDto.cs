namespace Core.Dtos;

public class FilmSelectionDto
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string> Films { get; private set; } = new List<string>();

}
