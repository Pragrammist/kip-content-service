using Microsoft.AspNetCore.Mvc;
using Core.Repositories;
using Web.Models;

namespace Web.Controllers;

[ApiController]
[Route("censors")]
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
    /// <param name="skip">сколько взять</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken token, [FromQuery(Name = "_end")]int limit = 10, [FromQuery(Name = "_start")]int skip = 0)
    {
        
        var censors = (await _censorRepo.Get(limit, skip, token)).ToArray();
        Response.Headers.Add("X-Total-Count", censors.Count().ToString());
        Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
        return new ObjectResult(censors);
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
    /// <param name="censorModel">модель цезора при создании</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateCensorModel censorModel, CancellationToken token)
    {
        var censor = await _censorRepo.Create(censorModel.Name, token, censorModel.Films);
        return  new ObjectResult(censor);
    }

    /// <summary>
    /// Меняет данные у фильма
    /// </summary>
    /// <param name="censorEditModel">модель редактирования</param>
    /// <param name="id">айди цензора</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(string id, [FromBody]EditCensorModel censorEditModel, CancellationToken token)
    {
        var isSuccess = true;
        if(censorEditModel.Name is not null)
            isSuccess = await _censorRepo.ChangeName(censorEditModel.Name, id);
        
        if(censorEditModel.Films is not null)
            isSuccess = await _censorRepo.SetFilmsTop(id, censorEditModel.Films);

        if(isSuccess)
            return Ok();
        else
            return BadRequest("Что не поменялось");
    }


}