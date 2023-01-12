using Core.Repositories;

namespace Core.Interactors;

public class FilmInteractor
{
    readonly FilmRepository _filmRepository;
    readonly EntityFilmRepository _entityFilmRepository;
    public FilmInteractor(FilmRepository filmRepository, EntityFilmRepository entityFilmRepository)
    {
        _filmRepository = filmRepository;
        _entityFilmRepository = entityFilmRepository;
    }

    public async Task AddScore(string id, uint score, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.AddScore(score);

        await _filmRepository.UpdateScore(id, film.Score, film.ScoreCount, token);
    }

    public async Task IncrWillWatchCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.IncrWillWatchCount();

        await _filmRepository.UpdateWillWatchCount(id, film.WillWatchCount);
    }

    public async Task DecrWillWatchCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.DecrWillWatchCount();

        await _filmRepository.UpdateWillWatchCount(id, film.WillWatchCount);
    }


    public async Task IncrShareCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.IncrShareCountCount();

        await _filmRepository.UpdateShareCount(id, film.ShareCount);
    }


    public async Task IncrViewsCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.IncrViewCount();

        await _filmRepository.UpdateViewCount(id, film.ViewCount);
    }


    public async Task IncrNotInterestingCount(string id, CancellationToken token = default)
    {   
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.IncrNotInterestingCount();

        await _filmRepository.UpdateNotInterestingCount(id, film.NotInterestingCount);
    }

    public async Task DecrNotInterestingCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.DecrNotInterestingCount();

        await _filmRepository.UpdateNotInterestingCount(id, film.NotInterestingCount);
    }


    public async Task IncrWatchedCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.IncrWatchedCount();

        await _filmRepository.UpdateWatchedCount(id, film.WatchedCount);
    }

    public async Task DecrWatchedCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return;

        film.DecrWatchedCount();

        await _filmRepository.UpdateWatchedCount(id, film.WatchedCount);
    }
}