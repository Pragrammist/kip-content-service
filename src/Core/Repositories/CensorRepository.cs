using Core.Dtos;

namespace Core.Repositories;

public interface CensorRepository
{
    Task<CensorDto?> Create(string name, CancellationToken token = default, List<string>? films = null);

    Task<IEnumerable<CensorDto>> Get(int limit, int skip, CancellationToken token = default);

    Task<CensorDto?> Get(string id, CancellationToken token = default);

    Task<bool> Delete(string id, CancellationToken token = default);

    Task<bool> ChangeName(string id, string name, CancellationToken token = default);

    Task<bool> SetFilmsTop(string id, List<string> films, CancellationToken token = default);

    Task<bool> DeleteFilm(string id, string filmId, CancellationToken token = default);
}
