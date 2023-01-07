

namespace Core.Interfaces;

public interface PersonRepository
{
    void Create(object data);

    object Get(object data);

    void Delete(object data);

    void AddNomination(object data);

    void DeleteNomination(object data);

    void AddFilm(object data);

    void DeleteFilm(object data);

    void UpdateData(object data);
}
