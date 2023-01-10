namespace Core.Dtos.CreateFilmDtos;

public class CreateFilmDto
{
    public uint AgeLimit { get; set; }
    
    public string Banner { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int KindOfFilm { get; set; }

    public int ReleaseType { get; set; }

    public TimeSpan? Duration { get; set; } 

    public DateTime? Release { get; set; }

    public DateTime? StartScreening { get; set; }

    public DateTime? EndScreening { get; set; }

    public string? Content { get; set; } 

    public int? Fees { get; set; }

    public List<string> Images { get; set; } = new List<string>();

    public List<string> Stuff { get; set; } = new List<string>();

    public List<string> Articles { get; set; }  = new List<string>();

    public List<string> Trailers { get; set; } = new List<string>();

    public List<string> Tizers { get; set; }  = new List<string>();

    public List<string> RelatedFilms { get; set; }  = new List<string>();

    public List<string> Genres { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();

    public List<Season> Seasons { get; set; } = new List<Season>();
}