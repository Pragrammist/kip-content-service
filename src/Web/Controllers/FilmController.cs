using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("film")]
public class FilmController : ControllerBase
{
    
    /// <summary>
    /// Возвращает фильм по айди
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Возвратился объект цензора</response>
    /// <response code="404">Не найден фильм</response>
    [HttpGet("{filmId}")]
    public IActionResult Get(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Возвращает коллекцию цензоров
    /// </summary>
    /// <param name="limit">максимальное количество за раз</param>
    /// <param name="page">номер страницы</param>
    /// <response code="200">Дает коллекцию цензоров. Коллекция может бы быть пустая</response>
    [HttpGet("/films/{limit?}/{page?}")]
    public IActionResult Get(uint limit = 20, uint page = 1)
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    /// <summary>
    /// Удаляет фильм по айди
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Не найден фильм</response>
    [HttpDelete("{filmId}")]
    public IActionResult Delete(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }
    
    /// <summary>
    /// Создает фильм
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
    /// Меняет некоторую информацию фильмы
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
    /// Увеличивает счетчик "неинтересно" на 1 единицу.
    /// Нет проверки на уникальность того, кто увеличивает это счетчик.
    /// Поэтому метод не для админки
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("notinteresting/{filmId}")]
    public IActionResult IncrNotInteresting(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Уменьшает счетчик "неинтересно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("interesting/{filmId}")]
    public IActionResult DecrNotInteresting(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Увеличивает счетчик "просмотра" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("view/{filmId}")]
    public IActionResult IncrViewsCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Увеличивает счетчик "буду смотреть" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("willwatch/{filmId}")]
    public IActionResult IncrWillWatchCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Уменьшает счетчик "буду смотреть" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("willnotwatch/{filmId}")]
    public IActionResult DecrWillWatchCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Увеличивает счетчик "поделилось" на 1 единицу.
    /// Не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("share/{filmId}")]
    public IActionResult IncrShareCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Увеличивает счетчик "просмотренно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Поэтому метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("watched/{filmId}")]
    public IActionResult IncrWatchedCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Уменьшает счетчик "просмотренно" на 1 единицу.
    /// Нет проверки на уникальность того, кто уменьшает это счетчик.
    /// Метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("unwatched/{filmId}")]
    public IActionResult DecrWatchedCount(string filmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Записывает оценку пользователя, увеличивая счетчик количество оценок
    /// Нет проверки на уникальность того, кто оцнивает
    /// Метод не для админки.
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="score">оценка пользоваетля</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("score/{filmId}/{score}")]
    public IActionResult Score(string filmId, double score)
    {
        return new ObjectResult("I AM OBJECT");
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
    /// <param name="filmId">айди фильма</param>
    /// <param name="image">айди фотографии</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("images/{filmId}/{image}")]
    public IActionResult AddImage(string filmId, string image)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Убирает фотографию из фильма
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="image">айди фотографии</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("images/{filmId}/{image}")]
    public IActionResult DeleteImage(string filmId, string image)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Дабавляет участника к фильму(актер, режисер и пр.)
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="person">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("stuff/{filmId}/{person}")]
    public IActionResult AddStuff(string filmId, string person)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Убирает участника из фильма
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="person">айди персоны</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("stuff/{filmId}/{person}")]
    public IActionResult DeleteStuff(string filmId, string person)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет статью связанную с фильмом
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="article">ссылка на статью</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("articles/{filmId}/{article}")]
    public IActionResult AddArticle(string filmId, string article)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Удаляет статью связанную с фильмом
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="article">ссылка на статью</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("articles/{filmId}/{article}")]
    public IActionResult DeleteArticle(string filmId, string article)
    {
        return new ObjectResult("I AM OBJECT");
    }
    

    /// <summary>
    /// Добавляет трейлер связанный с фильмом
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="trailer">айди трейлера</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("trailers/{filmId}/{trailer}")]
    public IActionResult AddTrailers(string filmId, string trailer)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Удаляет трейлер связанный с фильмом
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="trailer">айди трейлера</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("trailers/{filmId}/{trailer}")]
    public IActionResult DeleteTrailers(string filmId, string trailer)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Добавляет тизер к фильму
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="tizer">айди тизер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("tizers/{filmId}/{tizer}")]
    public IActionResult AddTizer(string filmId, string tizer)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Убирает тизер из фильма
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="tizer">айди тизер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("tizers/{filmId}/{tizer}")]
    public IActionResult DeleteTizer(string filmId, string tizer)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет связанный фильм к фильму
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="relateFilmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("relatefilms/{filmId}/{relateFilmId}")]
    public IActionResult AddRelatedFilm(string filmId, string relateFilmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Удаляет связанный фильм
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="relateFilmId">айди фильма</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("relatefilms/{filmId}/{relateFilmId}")]
    public IActionResult DeleteRelatedFilm(string filmId, string relateFilmId)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет номинацию к фильму
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("nominations/{filmId}/{nomination}")]
    public IActionResult AddNomination(string filmId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Удаляет номинацию из фильма
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="nomination">номинация</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("nominations/{filmId}/{nomination}")]
    public IActionResult DeleteNomination(string filmId, string nomination)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет сезон
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="num">номер</param>
    /// <param name="banner">банер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("seasons/{filmId}/{num}/{banner}")]
    public IActionResult AddSeason(string filmId, uint num, string banner)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Удаляет сезон
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="num">номер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("seasons/{filmId}/{num}")]
    public IActionResult DeleteSeason(string filmId, uint num)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет серию к сезону
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="num">номер</param>
    /// <param name="file">айди файла</param>
    /// <param name="season">номер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("serias/{filmId}/{season}/{num}/{file}")]
    public IActionResult AddSeria(string filmId, uint num, string file, uint season)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Удаляет серию из сезона
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="num">номер</param>
    /// <param name="season">номер</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("serias/{filmId}/{season}/{num}")]
    public IActionResult DeleteSeria(string filmId, uint num, uint season)
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет несколько сезонов с сериями сразу. 
    /// </summary>
    /// <remark>
    /// 
    /// </remark>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>    
    [HttpPut("seasons/{filmId}")]
    public IActionResult AddSeasonsAndSerias()
    {
        return new ObjectResult("I AM OBJECT");
    }


    /// <summary>
    /// Добавляет жанр к фильму
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="genre">жанр</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpPut("genres/{filmId}/{genre}")]
    public IActionResult AddGenre(string filmId, string genre)
    {
        return new ObjectResult("I AM OBJECT");
    }

    /// <summary>
    /// Удаляет жанр из фильма
    /// </summary>
    /// <param name="filmId">айди фильма</param>
    /// <param name="genre">жанр</param>
    /// <response code="200">Все хорошо. Операция была произведена успешно</response>
    /// <response code="404">Айди не найдено</response>
    [HttpDelete("genres/{filmId}/{genre}")]
    public IActionResult DeleteGenre(string filmId, string genre)
    {
        return new ObjectResult("I AM OBJECT");
    }
}
