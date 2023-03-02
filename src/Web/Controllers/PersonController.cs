using Microsoft.AspNetCore.Mvc;
using Core.Repositories;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos.UpdatePersonDtos;

namespace Web.Controllers;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase
{
    PersonRepository _personRepo;
    public PersonController(PersonRepository personRepo)
    {
        _personRepo = personRepo;
    }
    /// <summary>
    /// Возвращает персону по id.
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найдена персона</response>
    [HttpGet("{personId}")]
    public async Task<IActionResult> Get(string personId, CancellationToken token)
    {
        var person = await _personRepo.Get(personId, token);
        if(person is null)
            return NotFound();
        return new ObjectResult(person);
    }

    /// <summary>
    /// Возвращает коллекцию персон
    /// </summary>
    /// <param name="limit">максимальное количество возвращаемых элементов</param>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <param name="page">номер страницы</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/persons/{limit?}/{page?}")]
    public async Task<IActionResult> Get(CancellationToken token, uint limit = 20, uint page = 1)
    {
        var persons = await _personRepo.Get(limit, page, token);
        return new ObjectResult(persons);
    }

    
    /// <summary>
    /// Удаляет персону по айди
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден цензор</response>
    [HttpDelete("{personId}")]
    public async Task<IActionResult> Delete(string personId, CancellationToken token)
    {
        var isSuccess = await _personRepo.Delete(personId, token);
        return isSuccess ? Ok() : BadRequest();
    }
    
    /// <summary>
    /// Создает персону
    /// </summary>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <param name="personData">данные о персоне</param>
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
    public async Task<IActionResult> Create(CreatePersonDto personData, CancellationToken token)
    {
        var person = await _personRepo.Create(personData, token);
        return new ObjectResult(person);
    }
    
    /// <summary>
    /// Меняет некоторую информацию персоны
    /// </summary>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <param name="personId">адйи персоны</param>
    /// <param name="personDataDto">Данные чтобы обновить</param>
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
    [HttpPut("{personId}")]
    public async Task<IActionResult> ChangeInfo(string personId, UpdatePersonDto personDataDto, CancellationToken token)
    {
        var person = await _personRepo.UpdateData(personId, personDataDto, token);
        return new ObjectResult(person);
    }

    /// <summary>
    /// Добавляет номинацию для персоны
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="nomination">номинация</param>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpPut("nominations/{personId}/{nomination}")]
    public async Task<IActionResult> AddNomination(string personId, string nomination, CancellationToken token)
    {
        var iSsuccess = await _personRepo.AddNomination(personId, nomination, token);
        return iSsuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Убирает номинацию у персоны
    /// </summary>
    /// <param name="personId">айди персоны</param>
    /// <param name="nomination">номинация</param>
    /// <param name="token">токен, чтобы отменить запрос. Сам пердается, не обрщать на него внимание</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    /// <response code="404">Не нашел персону</response>
    [HttpPut("nominations/delete/{personId}/{nomination}")]
    public async Task<IActionResult> DeleteNomination(string personId, string nomination, CancellationToken token)
    {
        var iSsuccess = await _personRepo.DeleteNomination(personId, nomination, token);
        return iSsuccess ? Ok() : BadRequest();
    }
}
