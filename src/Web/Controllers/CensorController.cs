using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("censor")]
public class CensorController : ControllerBase
{
    /// <summary>
    /// Возвращает нензора по айди
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найден цензор</response>
    [HttpGet("{censorId}")]
    public IActionResult Get(string censorId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    /// <summary>
    /// Возвращает коллекцию цензоров
    /// </summary>
    /// <param name="limit">максимальное количество за раз</param>
    /// <param name="page">номер страницы</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/censors/{limit?}/{page?}")]
    public IActionResult Get(uint limit = 20, uint page = 1)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Удаляет цензора по айди
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден цензор</response>
    [HttpDelete("{censorId}")]
    public IActionResult Delete(string censorId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    /// <summary>
    /// Создает цензора
    /// </summary>
    /// <remarks>
    /// Прмер запроса:
    ///
    ///     POST /censor
    ///     {
    ///        
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPost]
    public IActionResult Create()
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Добавляет фильм к цензору
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="filmdId">айди фильма</param>
    /// <param name="place">порядок в топе</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpPut("film/{censorId}/{filmdId}/{place}")]
    public IActionResult AddFilm(string filmdId, string censorId, uint place)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Убирает фильзм у цензора
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="filmdId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел фильм или цензора</response>
    [HttpDelete("film/{censorId}/{filmdId}")]
    public IActionResult DeleteFilm(string filmdId, string censorId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Меняет имя цензора
    /// </summary>
    /// <param name="censorId">айди цензора</param>
    /// <param name="name">новое имя</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не нашел цензора</response>
    [HttpPut("name/{censorId}/{name}")]
    public IActionResult ChangeName(string censorId, string name)
    {
        return new ObjectResult("I AM OBJECT");
    }
}