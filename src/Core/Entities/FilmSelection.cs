namespace Core;

public class FilmSelection
{
    private FilmSelection(){}
    public FilmSelection(string name, string? id = null, List<string>? films = null){
        if(string.IsNullOrEmpty(name))
             throw new FieldIsNullOrEmptyException(nameof(Name), nameof(Person));
        Name = name;
        Films = films ?? new List<string>();
    }
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string> Films { get; private set; } = new List<string>();

}

