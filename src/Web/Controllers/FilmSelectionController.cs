using Microsoft.AspNetCore.Mvc;
using Core.Repositories;
using Web.Models;

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
    [HttpGet("{limit?}/{page?}")]
    public async Task<IActionResult> Get(CancellationToken token, uint limit = 20, uint page = 1)
    {
        var selections = (await _selectionRepo.Get(limit, page, token)).ToArray();
        Response.Headers.Add("X-Total-Count", selections.Count().ToString());
        Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
        return new ObjectResult(selections);
    }

    /// <summary>
    /// Меняет имя цензора
    /// </summary>
    /// <param name="editModel">модель если поменять модели</param>
    /// <param name="id">модель если поменять модели</param>
    /// <param name="token">токен для сброса</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не нашел цензора</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> EditSelection(string id, EditSelectionModel editModel, CancellationToken token)
    {
        var isSuccess = true; 
        
        if(editModel.Name is not null)
            isSuccess = await _selectionRepo.ChangeName(id, editModel.Name);

        if(editModel.Films is not null)
            isSuccess = await _selectionRepo.SetFilms(id, editModel.Films, token);
        
        if(isSuccess)
            return Ok();
        else
            return BadRequest();
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
    /// Создает подборку
    /// </summary>
    /// <param name="selectionModel">модель подборки</param>
    /// <param name="token">токен для отмены запроса. Его не нужно передавать, он сам передается</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="400">Не прошло валидацию</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateSelectionModel selectionModel, CancellationToken token)
    {
        var censor = await _selectionRepo.Create(selectionModel.Name, token, selectionModel.Films);
        return  new ObjectResult(censor);
    }

    

}