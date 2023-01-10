namespace Core.Dtos.UpdateFilmDtos;

public class UpdateFilmDto
{
    public uint? AgeLimit { get; set; }
    
    public string? Banner { get; set; } 

    public string? Name { get; set; } 

    public string? Description { get; set; } 

    public string? Country { get; set; }  

    public string? KindOfFilm { get; set; } 

    public string? ReleaseType { get; set; }  

    public TimeSpan? Duration { get; set; } 

    public DateTime? Release { get; set; }

    public DateTime? StartScreening { get; set; }

    public DateTime? EndScreening { get; set; }

    public string? Content { get; set; } 

    public int? Fees { get; set; } 
}