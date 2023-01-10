namespace Core.Dtos;

public class PersonDto
{
    public string Id { get; set; }  = null!;

    public string KindOfPerson { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string Name { get; set; }  = null!; 

    public string Photo { get; set; } = null!;

    public uint Height { get; set; }

    public string Career { get; set; } = null!; 

    public string BirthPlace { get; set; } = null!;

    public List<string> Films { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();

}