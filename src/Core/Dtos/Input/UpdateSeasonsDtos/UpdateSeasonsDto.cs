
namespace Core.Dtos.UpdateSeasonsDtos;

public class UpdateSeasonDto
{
    public uint Num { get; set; }

    public List<UpdateSeriaDto> Serias { get; set; } = new List<UpdateSeriaDto>();

    public string Banner { get; set; } = null!;
}
