namespace Core;

public class Person //знаменитость
{

    private Person(){}

    public Person(string name, DateTime birthday, string photo, 
    string career, string birthPlace, uint height, 
    string? id = null, PersonType kindOfPerson = default, 
    List<string>? nominations = null, List<string>? films = null)
    {
        Films = films ?? new List<string>();
        Nominations = nominations ?? new List<string>();
        KindOfPerson = kindOfPerson; 
        KindOfPerson = kindOfPerson;
        Name = name;
        Photo = photo;
        Height = height;
        BirthPlace = birthPlace;   
    }
    public string Id { get; set; }  = null!;

    public PersonType KindOfPerson { get; set; }

    public DateTime Birthday { get; set; }

    public string Name { get; set; }  = null!; 

    public string Photo { get; set; } = null!;

    public uint Height { get; set; }

    public string Career { get; set; } = null!; 

    public string BirthPlace { get; set; } = null!;

    public List<string> Films { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();
}
