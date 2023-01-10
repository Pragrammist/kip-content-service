namespace Core.Dtos;

public class FilmDto
{
    public bool IsSerial { get; set; }

    public bool IsFilm { get; set; }

    public bool IsReleas { get; set; }

    public bool IsPremiera { get; set; }

    public bool IsSreening { get; set; }


    public uint AgeLimit { get; set; }


    public string Id { get; set; } = null!;
    
    public string Banner { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Country { get; set; } = null!;

    

    public string KindOfFilm { get; set; } = null!;

    public string ReleaseType { get; set; } = null!;


    public TimeSpan? Duration { get; set; } 

    public DateTime? Release { get; set; }

    public DateTime? StartScreening { get; set; }

    public DateTime? EndScreening { get; set; }

    public string? Content { get; set; } 

    public int? Fees { get; set; } // сборы



    public double Score { get; set; }

    public uint ScoreCount { get; set; }

    public uint WillWatchCount { get; set; }

    public uint ShareCount { get; set; }

    public uint WatchedCount { get; set; }

    public uint ViewCount { get; set; }

    public uint NotInterestingCount { get; set; }


    public List<string> Images { get; set; } = new List<string>();

    public List<string> Stuff { get; set; } = new List<string>();

    public List<string> Articles { get; set; }  = new List<string>();

    public List<string> Trailers { get; set; } = new List<string>();

    public List<string> Tizers { get; set; }  = new List<string>();

    public List<string> RelatedFilms { get; set; }  = new List<string>();

    public List<string> Genres { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();

    public List<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();

}
