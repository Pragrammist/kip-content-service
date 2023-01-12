namespace Core.Dtos.CreateFilmDtos;

public class CreateSeasonDto
{    public uint Num { get; set; }

    public List<CreateSeriaDto> Serias { get; set; } = new List<CreateSeriaDto>();

    public string Banner { get; set; } = null!;
}
