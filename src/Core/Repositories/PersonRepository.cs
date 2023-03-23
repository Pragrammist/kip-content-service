using Core.Dtos.UpdatePersonDtos;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos;

namespace Core.Repositories;

public interface PersonRepository
{
    Task<PersonDto> Create(CreatePersonDto person, CancellationToken token = default);


    Task<PersonDto?> Get(string id, CancellationToken token = default);


    Task<IEnumerable<PersonDto>> Get(int limit, int skip, CancellationToken token = default);


    Task<bool> Delete(string id, CancellationToken token = default);


    


    Task<bool> AddNomination(string id, string nomination, CancellationToken token = default);

    Task<bool> DeleteNomination(string id, string nomination, CancellationToken token = default);

    Task<bool> UpdateData(string id, UpdatePersonDto dataToUpdate, CancellationToken token = default);
}
