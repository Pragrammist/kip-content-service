using Core.Dtos.UpdatePersonDtos;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos;

namespace Core.Interfaces;

public interface PersonRepository
{
    void Create(CreatePersonDto person);


    PersonDto Get(string personId);


    IEnumerable<PersonDto> Get(uint limit = 20, uint page = 1);


    void Delete(string id);


    void AddFilm(string id, string filmId);

    void DeleteFilm(string id, string filmId);


    void AddNomination(string id, string filmId);

    void DeleteNomination(string id, string filmId);

    void UpdateData(UpdatePersonDto dataToUpdate);
}
