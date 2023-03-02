using Microsoft.AspNetCore.Mvc;
using Core.Repositories;

namespace Web.Controllers;

[ApiController]
[Route("selections")]
public class FilmSelectionController : ControllerBase
{
    readonly FilmSelectionRepository _selectionRepo;
    public FilmSelectionController(FilmSelectionRepository selectionRepo)
    {
        _selectionRepo = selectionRepo;
    }
    /// <summary>
    /// Возвращает нензора по айди
    /// </summary>
    /// <param name="selectionId">айди подборок</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найден цензор</response>
    [HttpGet("{selectionId}")]
    public async Task<IActionResult> Get(string selectionId, CancellationToken token)
    {
        var censor = await _selectionRepo.Get(selectionId, token);
        
        if(censor is null || censor.Id is null)
            return NotFound();

        return new ObjectResult(censor);
    }
    /// <summary>
    /// Возвращает коллекцию цензоров
    /// </summary>
    /// <param name="limit">максимальное количество за раз</param>
    /// <param name="page">номер страницы</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/selections/{limit?}/{page?}")]
    public async Task<IActionResult> Get(CancellationToken token, uint limit = 20, uint page = 1)
    {
        var censor = await _selectionRepo.Get(limit, page, token);
        return new ObjectResult(censor);
    }

    /// <summary>
    /// Удаляет цензора по айди
    /// </summary>
    /// <param name="selectionId">айди подборок</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден цензор</response>
    [HttpDelete("{selectionId}")]
    public async Task<IActionResult> Delete(string selectionId, CancellationToken token)
    {
        var isSuccess = await _selectionRepo.Delete(selectionId, token);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }
    /// <summary>
    /// Создает цензора
    /// </summary>
    /// <param name="name">имя цензора</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <param name="films">фильмы, которые есть у цензора. При передаче нужно расположить фильмы в порядке топа самого цензора. Передавать id фильмов, НЕ НАЗВАНИЯ</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPost("{name}")]
    public async Task<IActionResult> Create(string name, CancellationToken token, List<string>? films = null)
    {
        var censor = await _selectionRepo.Create(name, token, films);
        return  new ObjectResult(censor);
    }

    /// <summary>
    /// Обновляет весь список топа фильмов цензора
    /// </summary>
    /// <param name="selectionId">айди подборок</param>
    /// <param name="filmId">Айди фильма</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpPut("films/{selectionId}/")]
    public async Task<IActionResult> AddFilm(string filmId, string selectionId, CancellationToken token)
    {
        var isSuccess = await _selectionRepo.AddFilm(selectionId, filmId);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }

    /// <summary>
    /// Убирает фильзм у цензора
    /// </summary>
    /// <param name="selectionId">айди подборок</param>
    /// <param name="filmdId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpDelete("films/{selectionId}/{filmdId}")]
    public async Task<IActionResult> DeleteFilm(string filmdId, string selectionId)
    {
        var isSuccess = await _selectionRepo.DeleteFilm(selectionId, filmdId);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }

    /// <summary>
    /// Меняет имя цензора
    /// </summary>
    /// <param name="selectionId">айди подборок</param>
    /// <param name="name">новое имя</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не нашел цензора</response>
    [HttpPut("name/{selectionId}/{name}")]
    public async Task<IActionResult> ChangeName(string selectionId, string name)
    {
        var isSuccess = await _selectionRepo.ChangeName(selectionId, name);
        
        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }
}