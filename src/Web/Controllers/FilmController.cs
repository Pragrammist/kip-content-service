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
}
