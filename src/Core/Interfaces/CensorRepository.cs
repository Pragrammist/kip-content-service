

namespace Core.Interfaces;

public interface CensorRepository
{
    void Create(object data);

    object Get(object data);

    object AddFilm(string film, string censorId);

    object DeleteFilm(string film, string censorId);

    void Delete(object data);
}
