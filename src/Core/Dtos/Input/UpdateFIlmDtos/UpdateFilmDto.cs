using Core.Dtos.UpdateSeasonsDtos;

namespace Core.Dtos.UpdateFilmDtos;

public class UpdateFilmDto
{
    public uint? AgeLimit { get; set; }
    
    public string? Banner { get; set; } 

    public string? Name { get; set; } 

    public string? Description { get; set; } 

    public string? Country { get; set; }  

    public int? KindOfFilm { get; set; } 

    public int? ReleaseType { get; set; }  

    public TimeSpan? Duration { get; set; } 

    public DateTime? Release { get; set; }

    public DateTime? StartScreening { get; set; }

    public DateTime? EndScreening { get; set; }

    public string? Content { get; set; } 

    public int? Fees { get; set; }


    public List<string>? Images { get; set; }


    public List<string>? Persons { get; set; }


    public List<string>? Articles { get; set; }


    public List<string>? Trailers { get; set; }


    public List<string>? Tizers { get; set; }


    public List<string>? RelatedFilms { get; set; }


    public List<string>? Nominations { get; set; }


    public List<UpdateSeasonDto>? newSeasons { get; set; }


    public List<string>? Genres { get; set; }
}