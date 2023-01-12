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
        var films = await _filmRepository.Get(limit, page);

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
        await _filmRepository.Delete(filmId, token);

        return Ok();
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
        var filmRes = await _filmRepository.UpdateData(filmId, film, token);

        return new ObjectResult(filmRes);
    }

    /// <summary>
    /// Увеличивает счетчик "неинтересно" на 1 единицу.
    /// Нет проверки на уникальность того, кто увеличивает это счетчик.
    /// Поэтому метод не для админки
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("notinteresting/{filmId}")]
    public async Task<IActionResult> IncrNotInteresting(string filmId, CancellationToken token)
    {
        await _interactor.IncrNotInterestingCount(filmId, token);

        return Ok();
    }

    /// <summary>
    /// Уменьшает счетчик "неинтересно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("interesting/{filmId}")]
    public async Task<IActionResult> DecrNotInteresting(string filmId, CancellationToken token)
    {
        await _interactor.DecrNotInterestingCount(filmId, token);
        
        return Ok();
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
        await _interactor.IncrViewsCount(filmId, token);
        
        return Ok();
    }


    /// <summary>
    /// Увеличивает счетчик "буду смотреть" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("willwatch/{filmId}")]
    public async Task<IActionResult> IncrWillWatchCount(string filmId, CancellationToken token)
    {
        await _interactor.IncrWillWatchCount(filmId, token);
        
        return Ok();
    }


    /// <summary>
    /// Уменьшает счетчик "буду смотреть" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("willnotwatch/{filmId}")]
    public async Task<IActionResult> DecrWillWatchCount(string filmId, CancellationToken token)
    {
        await _interactor.DecrWillWatchCount(filmId, token);
        
        return Ok();
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
        await _interactor.IncrShareCount(filmId, token);
        
        return Ok();
    }


    /// <summary>
    /// Увеличивает счетчик "просмотренно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("watched/{filmId}")]
    public async Task<IActionResult> IncrWatchedCount(string filmId, CancellationToken token)
    {
        await _interactor.IncrWatchedCount(filmId, token);
        
        return Ok();
    }

    /// <summary>
    /// Уменьшает счетчик "просмотренно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("unwatched/{filmId}")]
    public async Task<IActionResult> DecrWatchedCount(string filmId, CancellationToken token)
    {
        await _interactor.DecrWatchedCount(filmId, token);
        
        return Ok();
    }


    /// <summary>
    /// Записывает оценку пользователя, увеличивая счетчик количество оценок
    /// Нет проверки на уникальность того, кто оцнивает
    /// Метод не для админки.
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="score">оценка пользоваетля</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("score/{filmId}/{score}")]
    public async Task<IActionResult> Score(string filmId, uint score, CancellationToken token)
    {
        await _interactor.AddScore(filmId, score, token);
        
        return Ok();
    }

    // [HttpPut("unscore/{filmId}/{score}")]
    // public IActionResult UnScore(string filmId, double score)
    // {
    //     return new ObjectResult("I AM OBJECT");
    // }



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
        await _filmRepository.AddImage(filmId, image, token);
        return Ok();
    }

    /// <summary>
    /// Убирает фотографию из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="image">айди фотографии</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("images/{filmId}/{image}")]
    public async Task<IActionResult> DeleteImage(string filmId, string image, CancellationToken token)
    {
        await _filmRepository.DeleteImage(filmId, image, token);
        return Ok();
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
    public async Task<IActionResult> AddStuff(string filmId, string personId, CancellationToken token)
    {
        await _filmRepository.AddPerson(filmId, personId, token);
        return Ok();
    }

    /// <summary>
    /// Убирает участника из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="personId">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("stuff/{filmId}/{person}")]
    public async Task<IActionResult> DeleteStuff(string filmId, string personId, CancellationToken token)
    {
        await _filmRepository.DeletePerson(filmId, personId, token);
        return Ok();
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
        await _filmRepository.AddArticle(filmId, article, token);
        return Ok();
    }


    /// <summary>
    /// Удаляет статью связанную с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="article">ссылка на статью</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("articles/{filmId}/{article}")]
    public async Task<IActionResult> DeleteArticle(string filmId, string article, CancellationToken token)
    {
        await _filmRepository.DeleteArticle(filmId, article, token);
        return Ok();
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
        await _filmRepository.AddTrailer(filmId, trailer, token);
        return Ok();
    }


    /// <summary>
    /// Удаляет трейлер связанный с фильмом
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="trailer">айди трейлера</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("trailers/{filmId}/{trailer}")]
    public async Task<IActionResult> DeleteTrailer(string filmId, string trailer, CancellationToken token)
    {
        await _filmRepository.DeleteTrailer(filmId, trailer, token);
        return Ok();
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
        await _filmRepository.AddTizer(filmId, tizer, token);
        return Ok();
    }

    /// <summary>
    /// Убирает тизер из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="tizer">айди тизер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("tizers/{filmId}/{tizer}")]
    public async Task<IActionResult> DeleteTizer(string filmId, string tizer, CancellationToken token)
    {
        await _filmRepository.DeleteTizer(filmId, tizer, token);
        return Ok();
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
        await _filmRepository.AddRelatedFilm(filmId, relateFilmId, token);
        return Ok();
    }


    /// <summary>
    /// Удаляет связанный фильм
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="relateFilmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("relatefilms/{filmId}/{relateFilmId}")]
    public async Task<IActionResult> DeleteRelatedFilm(string filmId, string relateFilmId, CancellationToken token)
    {
        await _filmRepository.DeleteRelatedFilm(filmId, relateFilmId, token);
        return Ok();
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
        await _filmRepository.AddNomination(filmId, nomination, token);
        return Ok();
    }


    /// <summary>
    /// Удаляет номинацию из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("nominations/{filmId}/{nomination}")]
    public async Task<IActionResult> DeleteNomination(string filmId, string nomination, CancellationToken token)
    {
        await _filmRepository.DeleteNomination(filmId, nomination, token);
        return Ok();
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
        await _filmRepository.UpdateSeasonAndSerias(filmId, newSeasons, token);
        return Ok();
    }


    // /// <summary>
    // /// Удаляет сезон
    // /// </summary>
    // /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    // /// <param name="filmId">айди фильма</param>
    // /// <param name="num">номер</param>
    // /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    // /// <response code="404">Айди не найдено</response>
    // [HttpDelete("seasons/{filmId}/{num}")]
    // public async Task<IActionResult> DeleteSeason(string filmId, uint num, CancellationToken token)
    // {
    //     return new ObjectResult("I AM OBJECT");
    // }


    // /// <summary>
    // /// Добавляет серию к сезону
    // /// </summary>
    // /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    // /// <param name="filmId">айди фильма</param>
    // /// <param name="num">номер</param>
    // /// <param name="file">айди файла</param>
    // /// <param name="season">номер</param>
    // /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    // /// <response code="404">Айди не найдено</response>
    // [HttpPut("serias/{filmId}/{season}/{num}/{file}")]
    // public async Task<IActionResult> AddSeria(string filmId, uint num, string file, uint season, CancellationToken token)
    // {
    //     return new ObjectResult("I AM OBJECT");
    // }

    // /// <summary>
    // /// Удаляет серию из сезона
    // /// </summary>
    // /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    // /// <param name="filmId">айди фильма</param>
    // /// <param name="num">номер</param>
    // /// <param name="season">номер</param>
    // /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    // /// <response code="404">Айди не найдено</response>
    // [HttpDelete("serias/{filmId}/{season}/{num}")]
    // public async Task<IActionResult> DeleteSeria(string filmId, uint num, uint season, CancellationToken token)
    // {
    //     return new ObjectResult("I AM OBJECT");
    // }


    // /// <summary>
    // /// Добавляет несколько сезонов с сериями сразу. 
    // /// </summary>
    // /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    // /// <remark>
    // /// 
    // /// </remark>
    // /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    // /// <response code="404">Айди не найдено</response>    
    // [HttpPut("seasons/{filmId}")]
    // public async Task<IActionResult> AddSeasonsAndSerias(CancellationToken token)
    // {
    //     return new ObjectResult("I AM OBJECT");
    // }


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
        await _filmRepository.AddGenre(filmId, genre, token);
        return Ok();
    }

    /// <summary>
    /// Удаляет жанр из фильма
    /// </summary>
    /// <param name="token">токен для отмены запроса. Создается автоматически. Не обращать на него внимание</param>
    /// <param name="filmId">айди фильма</param>
    /// <param name="genre">жанр</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("genres/{filmId}/{genre}")]
    public async Task<IActionResult> DeleteGenre(string filmId, string genre, CancellationToken token)
    {
        await _filmRepository.DeleteGenre(filmId, genre, token);
        return Ok();
    }
}
