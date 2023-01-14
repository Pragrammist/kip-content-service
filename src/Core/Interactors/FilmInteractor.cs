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

    public async Task<bool> AddScore(string id, uint score, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.AddScore(score);

        return await _filmRepository.UpdateScore(id, film.Score, film.ScoreCount, token);
    }

    public async Task<bool> IncrWillWatchCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.IncrWillWatchCount();

        return await _filmRepository.UpdateWillWatchCount(id, film.WillWatchCount);
    }

    public async Task<bool> DecrWillWatchCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.DecrWillWatchCount();

        return await _filmRepository.UpdateWillWatchCount(id, film.WillWatchCount);
    }


    public async Task<bool> IncrShareCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.IncrShareCountCount();

        return await _filmRepository.UpdateShareCount(id, film.ShareCount);
    }


    public async Task<bool> IncrViewsCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.IncrViewCount();

        return await _filmRepository.UpdateViewCount(id, film.ViewCount);
    }


    public async Task<bool> IncrNotInterestingCount(string id, CancellationToken token = default)
    {   
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.IncrNotInterestingCount();

        return await _filmRepository.UpdateNotInterestingCount(id, film.NotInterestingCount);
    }

    public async Task<bool> DecrNotInterestingCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.DecrNotInterestingCount();

        return await _filmRepository.UpdateNotInterestingCount(id, film.NotInterestingCount);
    }


    public async Task<bool> IncrWatchedCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.IncrWatchedCount();

        return await _filmRepository.UpdateWatchedCount(id, film.WatchedCount);
    }

    public async Task<bool> DecrWatchedCount(string id, CancellationToken token = default)
    {
        var film = await _entityFilmRepository.Get(id, token);

        if(film is null)
            return false;

        film.DecrWatchedCount();

        return await _filmRepository.UpdateWatchedCount(id, film.WatchedCount);
    }
}