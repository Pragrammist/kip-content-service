using Core.Dtos;

namespace Core.Repositories;

public interface FilmSelectionRepository
{
    Task<FilmSelectionDto?> Create(string name, CancellationToken token = default, List<string>? films = null);

    Task<IEnumerable<FilmSelectionDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default);

    Task<FilmSelectionDto?> Get(string id, CancellationToken token = default);

    Task<bool> Delete(string id, CancellationToken token = default);

    Task<bool> ChangeName(string id, string name, CancellationToken token = default);

    Task<bool> AddFilm(string id, string filmId, CancellationToken token = default);

    Task<bool> DeleteFilm(string id, string filmId, CancellationToken token = default);
}
