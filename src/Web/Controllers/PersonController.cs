using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase
{
    
    /// <summary>
    /// Возвращает персону по id.
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найдена персона</response>
    [HttpGet("{personId}")]
    public IActionResult Get(string personId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Возвращает коллекцию персон
    /// </summary>
    /// <param name="limit">максимальное количество возвращаемых элементов</param>
    /// <param name="page">номер страницы</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/persons/{limit?}/{page?}")]
    public IActionResult Get(uint limit = 20, uint page = 1)
    {
        return new ObjectResult("I AM OBJECT");
    }

    
    /// <summary>
    /// Удаляет персону по айди
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден цензор</response>
    [HttpDelete("{personId}")]
    public IActionResult Delete(string personId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    /// <summary>
    /// Создает персону
    /// </summary>
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
    public IActionResult Create()
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    /// <summary>
    /// Меняет некоторую информацию персоны
    /// </summary>
    /// <remarks>
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
    [HttpPut]
    public IActionResult ChangeInfo()
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Добавляет номинацию для персоны
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpPut("nomination/{personId}/{nomination}")]
    public IActionResult AddNomination(string personId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Убирает номинацию у персоны
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpDelete("nomination/{personId}/{nomination}")]
    public IActionResult DeleteNomination(string personId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Добавляет фильм для персоны, в котором принимал учавствие
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpPut("film/{personId}/{filmId}")]
    public IActionResult AddFilm(string personId, string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    /// <summary>
    /// Убирает фильм у персоны, в котором принимал учавствие
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpDelete("film/{personId}/{filmId}")]
    public IActionResult DeleteFilm(string personId, string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }
}
