
namespace Core.Dtos.UpdatePersonDtos;

public class UpdatePersonDto
{
    public int? KindOfPerson { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Name { get; set; } 

    public string? Photo { get; set; } 

    public uint? Height { get; set; }

    public string? Career { get; set; } 

    public string? BirthPlace { get; set; } 

    public List<string>? Nominations { get; set; }
}