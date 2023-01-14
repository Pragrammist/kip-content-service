using Core.Dtos;

namespace Core.Repositories;

public interface CensorRepository
{
    Task<CensorDto> Create(string name, CancellationToken token = default, List<string>? films = null);

    Task<IEnumerable<CensorDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default);

    Task<CensorDto?> Get(string id, CancellationToken token = default);

    Task<bool> Delete(string id, CancellationToken token = default);

    Task<bool> ChangeName(string id, string name, CancellationToken token = default);

    //Task AddFilm(string id, string filmId, CancellationToken token = default);

    Task<bool> SetFilmsTop(string id, List<string> films, CancellationToken token = default);

    Task<bool> DeleteFilm(string id, string filmId, CancellationToken token = default);
}
