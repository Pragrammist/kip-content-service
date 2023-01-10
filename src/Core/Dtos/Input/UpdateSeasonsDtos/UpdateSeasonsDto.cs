
namespace Core.Dtos.UpdateSeasonsDtos;

public class UpdateSeasonsDto
{
    public uint Num { get; set; }

    public List<UpdateSeriasDto> Serias { get; set; } = new List<UpdateSeriasDto>();

    public string Banner { get; set; } = null!;
}
