namespace Core.Dtos;

public class SeasonDto
{
    public uint Num { get; set; }

    public List<SeriaDto> Serias { get; set; } = new List<SeriaDto>();

    public string Banner { get; set; } = null!;
}
