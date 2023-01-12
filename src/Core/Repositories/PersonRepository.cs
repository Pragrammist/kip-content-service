using Core.Dtos.UpdatePersonDtos;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos;

namespace Core.Repositories;

public interface PersonRepository
{
    Task<PersonDto> Create(CreatePersonDto person, CancellationToken token = default);


    Task<PersonDto?> Get(string id, CancellationToken token = default);


    Task<IEnumerable<PersonDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default);


    Task Delete(string id, CancellationToken token = default);


    Task AddFilm(string id, string filmId, CancellationToken token = default);

    Task DeleteFilm(string id, string filmId, CancellationToken token = default);


    Task AddNomination(string id, string nomination, CancellationToken token = default);

    Task DeleteNomination(string id, string nomination, CancellationToken token = default);

    Task<PersonDto> UpdateData(string id, UpdatePersonDto dataToUpdate, CancellationToken token = default);
}
