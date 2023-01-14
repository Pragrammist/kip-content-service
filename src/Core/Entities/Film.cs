namespace Core;

public class Film
{
    uint _ageLimit;

    private Film() {}

    public Film(string banner, string name, string description, string country, 
    FilmReleaseType releaseType = default, DateTime? release = null , DateTime? startScreening = null, 
    DateTime? endScreening = null, uint ageLimit = 0,FilmType kindOfFilm = default, 
    string? content = null, int? fees = null, TimeSpan? duration = null)
    {
        Banner = banner;
        Content = content;
        Name = name;
        AgeLimit = ageLimit;
        Description = description;
        Country = country;
        KindOfFilm = kindOfFilm;
        ReleaseType = releaseType;
        Duration = duration;
        Release = release;
        StartScreening = startScreening;
        EndScreening = endScreening;
        Fees = fees;
    }


    public bool IsSerial => KindOfFilm == FilmType.SERIAL;

    public bool IsFilm => KindOfFilm == FilmType.FILM;

    public bool IsReleas => ReleaseType == FilmReleaseType.RELEASE;

    public bool IsPremiera => ReleaseType == FilmReleaseType.PREMIERA;

    public bool IsSreening => ReleaseType == FilmReleaseType.SCREENING;


    public uint AgeLimit { get => _ageLimit; set => _ageLimit = value > 18 ? 18 : value; }


    public string Id { get; set; } = null!;
    
    public string Banner { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Country { get; set; } = null!;

    

    public FilmType KindOfFilm { get; set; }

    public FilmReleaseType ReleaseType { get; set; }


    public TimeSpan? Duration { get; set; } 

    public DateTime? Release { get; set; }

    public DateTime? StartScreening { get; set; }

    public DateTime? EndScreening { get; set; }

    public string? Content { get; set; } 

    public int? Fees { get; set; } // сборы



    public double Score { get; private set; }

    public uint ScoreCount { get; private set; }

    public uint WillWatchCount { get; private set; }

    public uint ShareCount { get; private set; }

    public uint WatchedCount { get; private set; }    

    public uint ViewCount { get; private set; }

    public uint NotInterestingCount { get; private set; }


    public List<string> Images { get; set; } = new List<string>();

    public List<string> Stuff { get; set; } = new List<string>();

    public List<string> Articles { get; set; }  = new List<string>();

    public List<string> Trailers { get; set; } = new List<string>();

    public List<string> Tizers { get; set; }  = new List<string>();

    public List<string> RelatedFilms { get; set; }  = new List<string>();

    public List<string> Genres { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();

    public List<Season> Seasons { get; set; } = new List<Season>();



    public void IncrNotInterestingCount() => NotInterestingCount++;
    
    public void DecrNotInterestingCount() 
    {
        if(NotInterestingCount < 1)
            return;
            
        NotInterestingCount--;
    }


    public void IncrViewCount() => ViewCount++;


    public void IncrWatchedCount() => WatchedCount++;

    public void DecrWatchedCount() 
    {
        if(WatchedCount < 1)
            return;

        WatchedCount--;
    }


    public void IncrShareCountCount() => ShareCount++;


    public void IncrWillWatchCount() => WillWatchCount++;
    
    public void DecrWillWatchCount()
    {
        if(WillWatchCount < 1)
            return;

        WillWatchCount--;
    }


    public void AddScore(uint score) 
    {
        if(score < 1 || score > 5)
            return;

        var beforeCount = ScoreCount;

        ScoreCount++;

        Score = ((Score * beforeCount) + score) / ScoreCount;
    }




    public static Film GetTestFilmWithDefaultValue() 
        => new Film("some banner", "some name", "some description", "some country");
    
}
