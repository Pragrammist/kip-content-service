using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("film")]
public class FilmController : ControllerBase
{
    
    [HttpGet("{filmId}")]
    public IActionResult Get(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpGet("/films/{limit}")]
    public IActionResult Get(uint limit)
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    [HttpDelete("{filmId}")]
    public IActionResult Delete(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    [HttpPost]
    public IActionResult Create()
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpPut]
    public IActionResult Change()
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("notinteresting/{filmId}")]
    public IActionResult IncrNotInteresting(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpPut("interesting/{filmId}")]
    public IActionResult DecrNotInteresting(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("view/{filmId}")]
    public IActionResult IncrViewsCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("willwatch/{filmId}")]
    public IActionResult IncrWillWatchCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpPut("willnotwatch/{filmId}")]
    public IActionResult DecrWillWatchCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("share/{filmId}")]
    public IActionResult IncrShareCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("watched/{filmId}")]
    public IActionResult IncrWatchedCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpPut("unwatched/{filmId}")]
    public IActionResult DecrWatchedCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("score/{filmId}/{score}")]
    public IActionResult Score(string filmId, double score)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpPut("unscore/{filmId}/{score}")]
    public IActionResult UnScore(string filmId, double score)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("images/{filmId}/{image}")]
    public IActionResult AddImage(string filmId, string image)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("images/{filmId}/{image}")]
    public IActionResult DeleteImage(string filmId, string image)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("stuff/{filmId}/{person}")]
    public IActionResult AddStuff(string filmId, string person)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("stuff/{filmId}/{person}")]
    public IActionResult DeleteStuff(string filmId, string person)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("articles/{filmId}/{article}")]
    public IActionResult AddArticle(string filmId, string article)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("articles/{filmId}/{article}")]
    public IActionResult DeleteArticle(string filmId, string article)
    {
        return new ObjectResult("I AM OBJECT");
    }
    

    [HttpPut("trailers/{filmId}/{trailer}")]
    public IActionResult AddTrailers(string filmId, string trailer)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("trailers/{filmId}/{trailer}")]
    public IActionResult DeleteTrailers(string filmId, string trailer)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("tizers/{filmId}/{tizer}")]
    public IActionResult AddTizer(string filmId, string tizer)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("tizers/{filmId}/{tizer}")]
    public IActionResult DeleteTizer(string filmId, string tizer)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("relatefilms/{filmId}/{relateFilmId}")]
    public IActionResult AddRelatedFilm(string filmId, string relateFilmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("relatefilms/{filmId}/{relateFilmId}")]
    public IActionResult DeleteRelatedFilm(string filmId, string relateFilmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("nominations/{filmId}/{nomination}")]
    public IActionResult AddNomination(string filmId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("nominations/{filmId}/{nomination}")]
    public IActionResult DeleteNomination(string filmId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("seasons/{filmId}/{num}/{banner}")]
    public IActionResult AddSeason(string filmId, uint num, string banner)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("seasons/{filmId}/{num}")]
    public IActionResult DeleteSeason(string filmId, uint num, string banner)
    {
        return new ObjectResult("I AM OBJECT");
    }


    [HttpPut("serias/{filmId}/{season}/{num}/{file}")]
    public IActionResult AddSeria(string filmId, uint num, string file, string season)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("serias/{filmId}/{season}/{num}")]
    public IActionResult DeleteSeria(string filmId, uint num, uint season)
    {
        return new ObjectResult("I AM OBJECT");
    }

    
    [HttpPut("seasons/{filmId}")]
    public IActionResult AddSeasonsAndSerias()
    {
        return new ObjectResult("I AM OBJECT");
    }



    [HttpPut("genres/{filmId}/{genre}")]
    public IActionResult AddGenre(string filmId, string genre)
    {
        return new ObjectResult("I AM OBJECT");
    }

    [HttpDelete("genres/{filmId}/{genre}")]
    public IActionResult DeleteGenre(string filmId, string genre)
    {
        return new ObjectResult("I AM OBJECT");
    }
}
