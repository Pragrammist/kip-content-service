using Core.Dtos;

namespace Core.Interfaces;

public interface CensorRepository
{
    void Create(string name, params string[] films);

    IEnumerable<CensorDto> Get(uint limit = 20, uint page = 1);

    CensorDto Get(string id);

    void Delete(string id);

    void ChangeName(string id, string name);

    void UpdateFilms(string id, params string[] newFilmsCollection);
}
