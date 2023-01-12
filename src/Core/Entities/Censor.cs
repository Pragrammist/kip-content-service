namespace Core;

public class Censor
{
    private Censor(){}
    public Censor(string name, string? id = null, List<string>? films = null){
        Name = name;
        Films = films ?? new List<string>();
    }
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string> Films { get; private set; } = new List<string>();

}
