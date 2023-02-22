using System.Security.Cryptography.X509Certificates;
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
        CheckNullOrEmpty();
        ValidateFieldData();
    }


    void ValidateFieldData()
    {
        // if(IsFilm && IsReleas && Content is null)
        //     throw new ContentIsNullException(IsFilm, IsReleas);
        
        // if (Duration is null && Content is not null)
        //     throw new DurationIsNullException();

        // if(IsSerial && IsReleas && Seasons.Count == 0)
        //     throw new SeasonCountIsZeroException(IsSerial, IsReleas, Seasons.Count);
        
    }

    void CheckNullOrEmpty()
    {
        if(string.IsNullOrEmpty(Banner))
            throw new FieldIsNullOrEmptyException(nameof(Banner), nameof(Film));

        if(string.IsNullOrEmpty(Name))
            throw new FieldIsNullOrEmptyException(nameof(Name), nameof(Film));
        
        if(string.IsNullOrEmpty(Description))
            throw new FieldIsNullOrEmptyException(nameof(Description), nameof(Film));
        
        if(string.IsNullOrEmpty(Country))
            throw new FieldIsNullOrEmptyException(nameof(Country), nameof(Film));
        
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

    List<string> _images = new List<string>();
    public List<string> Images { get => _images; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Images), nameof(Film));
        _images = value.ToList();
    } } 


    List<string> _stuff = new List<string>();
    public List<string> Stuff { get => _stuff; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Stuff), nameof(Film));
        _stuff = value.ToList();
    } } 


    List<string> _articles = new List<string>();
    public List<string> Articles { get => _articles; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Articles), nameof(Film));
        _articles = value.ToList();
    } } 


    List<string> _trailers = new List<string>();
    public List<string> Trailers { get => _trailers; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Trailers), nameof(Film));
        _trailers = value.ToList();
    } } 


    List<string> _tizers = new List<string>();
    public List<string> Tizers { get => _tizers; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Tizers), nameof(Film));
        _tizers = value.ToList();
    } }  


    List<string> _relatedFilms = new List<string>();
    public List<string> RelatedFilms { get => _relatedFilms; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(RelatedFilms), nameof(Film));
        _relatedFilms = value.ToList();
    } }  


    List<string> _genres = new List<string>();
    public List<string> Genres { get => _genres; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Genres), nameof(Film));
        _genres = value.ToList();
    } } 


    List<string> _nominations = new List<string>();
    public List<string> Nominations { get => _nominations; set {
        if(value.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(Nominations), nameof(Film));
        _nominations = value.ToList();
    } } 

    
    List<Season> _seasons = new List<Season>();
    public List<Season> Seasons { get => _seasons; set {
        _seasons = value.ToList();
    } } 



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

public class ContentIsNullException : Exception
{
    public ContentIsNullException (bool isFilm, bool isReleas): base($"IsFilm is {isFilm} and IsReleas is {isReleas} so content must not be null")
    {

    }
}

public class DurationIsNullException : Exception
{
    public DurationIsNullException () : base($"Duration of film is null when content is initializate")
    {

    }
}

public class SeasonCountIsZeroException : Exception
{
    public SeasonCountIsZeroException(bool isSerial,  bool isReleas, int seasonCount) : base($"IsSerial is {isSerial} and IsReleas is {isReleas} SeasonCount is {seasonCount}")
    {

    }
}
