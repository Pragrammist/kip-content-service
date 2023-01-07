namespace Core;

public class Censor
{
    private Censor(){}
    public Censor(string name, string? id = null){
        Name = name;
        Id = id ?? "";
    }
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string> Films { get; private set; } = new List<string>();

}
