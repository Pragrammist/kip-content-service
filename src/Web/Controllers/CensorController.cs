using Microsoft.AspNetCore.Mvc;
using Core.Repositories;

namespace Web.Controllers;

[ApiController]
[Route("censor")]
public class CensorController : ControllerBase
{

    readonly CensorRepository _censorRepo;
    public CensorController(CensorRepository censorRepo)
    {
        _censorRepo = censorRepo;
    }
    /// <summary>
    /// Возвращает нензора по айди
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найден цензор</response>
    [HttpGet("{censorId}")]
    public async Task<IActionResult> Get(string censorId, CancellationToken token)
    {
        var censor = await _censorRepo.Get(censorId, token);
        
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
    [HttpGet("/censors/{limit?}/{page?}")]
    public async Task<IActionResult> Get(CancellationToken token, uint limit = 20, uint page = 1)
    {
        var censor = await _censorRepo.Get(limit, page, token);
        return new ObjectResult(censor);
    }

    /// <summary>
    /// Удаляет цензора по айди
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден цензор</response>
    [HttpDelete("{censorId}")]
    public async Task<IActionResult> Delete(string censorId, CancellationToken token)
    {
        var isSuccess = await _censorRepo.Delete(censorId, token);

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
        var censor = await _censorRepo.Create(name, token, films);
        return  new ObjectResult(censor);
    }

    /// <summary>
    /// Обновляет весь список топа фильмов цензора
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="films">фильмы цензора. Располагать в порядке их топа. Передавать id фильмов, НЕ НАЗВАНИЯ</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpPut("films/{censorId}/")]
    public async Task<IActionResult> SetFilmsTop(List<string> films, string censorId, CancellationToken token)
    {
        var isSuccess = await _censorRepo.SetFilmsTop(censorId, films);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }

    /// <summary>
    /// Убирает фильзм у цензора
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="filmdId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpDelete("films/{censorId}/{filmdId}")]
    public async Task<IActionResult> DeleteFilm(string filmdId, string censorId)
    {
        var isSuccess = await _censorRepo.DeleteFilm(censorId, filmdId);

        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }

    /// <summary>
    /// Меняет имя цензора
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="name">новое имя</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не нашел цензора</response>
    [HttpPut("name/{censorId}/{name}")]
    public async Task<IActionResult> ChangeName(string censorId, string name)
    {
        var isSuccess = await _censorRepo.ChangeName(censorId, name);
        
        if(isSuccess)
            return Ok();
        else
            return BadRequest();
    }
}