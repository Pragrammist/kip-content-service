using Core.Repositories;
using Core.Interactors;
using Microsoft.AspNetCore.Mvc;
using Core.Dtos.CreateFilmDtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;

namespace Web.Controllers;

[ApiController]
[Route("film")]
public class FilmController : ControllerBase
{
    readonly FilmRepository _filmRepository;
    readonly FilmInteractor _interactor;
    public FilmController(FilmRepository filmRepository, FilmInteractor interactor)
    {   
        _filmRepository = filmRepository;
        _interactor = interactor;
    }

    /// <summary>
    /// Возвращает фильм по айди
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найден фильм</response>
    [HttpGet("{filmId}")]
    public async Task<IActionResult> Get(string filmId, CancellationToken token)
    {
        var film = await _filmRepository.Get(filmId);

        if(film is null)
            return NotFound();
        
        return new ObjectResult(film);
    }

    /// <summary>
    /// Возвращает коллекцию цензоров
    /// </summary>
    /// <param name="limit">максимальное количество за раз</param>
    /// <param name="page">номер страницы</param>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/films/{limit?}/{page?}")]
    public async Task<IActionResult> Get(CancellationToken token, uint limit = 20, uint page = 1)
    {
        var films = (await _filmRepository.Get(limit, page)).ToArray();
        Response.Headers.Add("X-Total-Count", films.Count().ToString());
        Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
        return new ObjectResult(films);
    }
    
    /// <summary>
    /// Удаляет фильм по айди
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден фильм</response>
    [HttpDelete("{filmId}")]
    public async Task<IActionResult> Delete(string filmId, CancellationToken token)
    {
        var isSuccess =  await _filmRepository.Delete(filmId, token);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }
    
    /// <summary>
    /// Создает фильм
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="film">данные фильма</param>
    /// <remarks>
    /// Прмер запроса:
    ///
    ///     POST /person
    ///     {
    ///        
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateFilmDto film, CancellationToken token)
    {
        var res = await _filmRepository.Create(film, token);
        return new ObjectResult(res);
    }

    /// <summary>
    /// Меняет некоторую информацию фильмы
    /// </summary>
    /// <remarks>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">фильм</param>
    /// <param name="film">данные фильма которые надо обновить</param>
    /// Прмер запроса:
    ///
    ///     PUT  /person
    ///     {
    ///        
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPut("{filmId}")]
    public async Task<IActionResult> ChangeInfo(string filmId, UpdateFilmDto film, CancellationToken token)
    {
        var isSuccess = await _filmRepository.UpdateData(filmId, film, token);
        
        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }

    


    /// <summary>
    /// Увеличивает счетчик "просмотра" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("view/{filmId}")]
    public async Task<IActionResult> IncrViewsCount(string filmId, CancellationToken token)
    {
        var isSuccess = await _interactor.IncrViewsCount(filmId, token);
        
        return isSuccess ? Ok() : BadRequest();
    }


   


    /// <summary>
    /// Увеличивает счетчик "поделилось" на 1 единицу.
    /// Не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("share/{filmId}")]
    public async Task<IActionResult> IncrShareCount(string filmId, CancellationToken token)
    {
        var isSuccess = await _interactor.IncrShareCount(filmId, token);
        
        return isSuccess ? Ok() : BadRequest();
    }


    

    



    /// <summary>
    /// Добавляет фотографию к фильму. Используется, чтобы показывать моменты из фильмов в фотографиях
    /// Нет проверки на уникальность
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="image">айди фотографии</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("images/{filmId}/{image}")]
    public async Task<IActionResult> AddImage(string filmId, string image, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddImage(filmId, image, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Убирает фотографию из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="image">айди фотографии</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("images/delete/{filmId}/{image}")]
    public async Task<IActionResult> DeleteImage(string filmId, string image, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteImage(filmId, image, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Дабавляет участника к фильму(актер, режисер и пр.)
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="personId">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("stuff/{filmId}/{personId}")]
    public async Task<IActionResult> AddPerson(string filmId, string personId, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddPerson(filmId, personId, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Убирает участника из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="personId">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("stuff/delete/{filmId}/{personId}")]
    public async Task<IActionResult> DeletePerson(string filmId, string personId, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeletePerson(filmId, personId, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Добавляет статью связанную с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="article">ссылка на статью</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("articles/{filmId}/{article}")]
    public async Task<IActionResult> AddArticle(string filmId, string article, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddArticle(filmId, article, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Удаляет статью связанную с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="article">ссылка на статью</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("articles/delete/{filmId}/{article}")]
    public async Task<IActionResult> DeleteArticle(string filmId, string article, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteArticle(filmId, article, token);
        return isSuccess ? Ok() : BadRequest();
    }
    

    /// <summary>
    /// Добавляет трейлер связанный с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="trailer">айди трейлера</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("trailers/{filmId}/{trailer}")]
    public async Task<IActionResult> AddTrailer(string filmId, string trailer, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddTrailer(filmId, trailer, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Удаляет трейлер связанный с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="trailer">айди трейлера</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("trailers/delete/{filmId}/{trailer}")]
    public async Task<IActionResult> DeleteTrailer(string filmId, string trailer, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteTrailer(filmId, trailer, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Добавляет тизер к фильму
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="tizer">айди тизер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("tizers/{filmId}/{tizer}")]
    public async Task<IActionResult> AddTizer(string filmId, string tizer, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddTizer(filmId, tizer, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Убирает тизер из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="tizer">айди тизер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("tizers/delete/{filmId}/{tizer}")]
    public async Task<IActionResult> DeleteTizer(string filmId, string tizer, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteTizer(filmId, tizer, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Добавляет связанный фильм к фильму
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="relateFilmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("relatefilms/{filmId}/{relateFilmId}")]
    public async Task<IActionResult> AddRelatedFilm(string filmId, string relateFilmId, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddRelatedFilm(filmId, relateFilmId, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Удаляет связанный фильм
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="relateFilmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("relatefilms/delete/{filmId}/{relateFilmId}")]
    public async Task<IActionResult> DeleteRelatedFilm(string filmId, string relateFilmId, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteRelatedFilm(filmId, relateFilmId, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Добавляет номинацию к фильму
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("nominations/{filmId}/{nomination}")]
    public async Task<IActionResult> AddNomination(string filmId, string nomination, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddNomination(filmId, nomination, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Удаляет номинацию из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("nominations/delete/{filmId}/{nomination}")]
    public async Task<IActionResult> DeleteNomination(string filmId, string nomination, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteNomination(filmId, nomination, token);
        return isSuccess ? Ok() : BadRequest();
    }


    /// <summary>
    /// Добавляет сезон
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="newSeasons">новые сезоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("seasons/{filmId}")]
    public async Task<IActionResult> UpdateSeasons(string filmId, List<UpdateSeasonDto> newSeasons, CancellationToken token)
    {
        var isSuccess = await _filmRepository.UpdateSeasonAndSerias(filmId, newSeasons, token);
        return isSuccess ? Ok() : BadRequest();
    }


   

    /// <summary>
    /// Добавляет жанр к фильму
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="genre">жанр</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("genres/{filmId}/{genre}")]
    public async Task<IActionResult> AddGenre(string filmId, string genre, CancellationToken token)
    {
        var isSuccess = await _filmRepository.AddGenre(filmId, genre, token);
        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Удаляет жанр из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="genre">жанр</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("genres/delete/{filmId}/{genre}")]
    public async Task<IActionResult> DeleteGenre(string filmId, string genre, CancellationToken token)
    {
        var isSuccess = await _filmRepository.DeleteGenre(filmId, genre, token);
        return isSuccess ? Ok() : BadRequest();
    }
}
