
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


    public void UpdateNominations(string id, params string[] newNominationsColl);


    public void UpdateFilms(string id, params string[] newFilmsColl);


    void UpdateData(UpdatePersonDto dataToUpdate);
}
