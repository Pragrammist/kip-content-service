namespace Core.Repositories;

public interface EntityFilmRepository
{
    Task<Film?> Get(string id, CancellationToken token = default);
}