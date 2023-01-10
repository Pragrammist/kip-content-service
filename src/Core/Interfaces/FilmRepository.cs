using Core.Dtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;

using Core.Dtos.CreateFilmDtos;

namespace Core.Interfaces;

public interface FilmRepository
{
    void Create(CreateFilmDto film);

    IEnumerable<FilmDto> Get(uint limit = 20, uint page = 1);

    FilmDto Get(string id);

    void UpdateData(UpdateFilmDto film); 

    void Delete(string id);


    public void UpdateImages(string id, params string[] newCollectionOfImages);
    

    public void UpdatePerson(string id, params string[] newCollectionOfPersons);

    
    public void UpdateArticles(string id, params string[] newCollectionOfArticles);

    
    public void UpdateTrailers(string id, params string[] newCollectionOfTrailers);


    public void UpdateTizers(string id, params string[] newCollectionOfTizers);
    

    public void UpdateRelatedFilms(string id, params string[] newCollectionOfFilms);


    public void UpdateGenres(string id, params string[] newCollectionOfGenres);


    public void UpdateNominations(string id, params string[] newCollectionOfNominations);


    public void UpdateSeasonAndSerias(string id, params UpdateSeasonsDto[] newCollectionOfSeasonWithSerias);  


    public void UpdateScore(string id, uint score, uint scoreCount);


    public void UpdateWillWatchCount(string id, uint willWatch);


    public void UpdateShareCount(string id, uint share);


    public void UpdateWatchedCount(string id, uint watched);


    public void UpdateViewCount(string id, uint view);


    public void UpdateNotInterestingCount(string id, uint notInteresting);
}
