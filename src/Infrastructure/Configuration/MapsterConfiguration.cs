using Core;
using Core.Dtos;
using Core.Dtos.CreateFilmDtos;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos.UpdateSeasonsDtos;
using Mapster;
namespace Infrastructure.Configuration;

public static class MapsterConfiguration
{
    public static void ConfigureMapsterGlobally()
    {
        CreatePersonConf();
        CreateFilmConf();
        UpdateSeasonsConf();
    }
    static void CreatePersonConf()
    {
        TypeAdapterConfig<CreatePersonDto, Person>
            .NewConfig().ConstructUsing(p => new Person(
                p.Name,
                p.Birthday,
                p.Photo,
                p.Career,
                p.BirthPlace,
                p.Height,
                null, //id
                (PersonType)p.KindOfPerson,
                p.Nominations,
                p.Films
            ));
    }
    static void CreateFilmConf()
    {
        TypeAdapterConfig<CreateSeriaDto, Seria>
            .NewConfig().ConstructUsing(s => new Seria(
                s.Num,
                s.IdFile
            ));
        
        TypeAdapterConfig<CreateSeasonDto, Season>
            .NewConfig().ConstructUsing(s => new Season(
                s.Banner,
                s.Num
            ));


        TypeAdapterConfig<CreateFilmDto, Film>
            .NewConfig().ConstructUsing(f => new Film(
                f.Banner,
                f.Name,
                f.Description,
                f.Country,
                (FilmReleaseType)f.ReleaseType,
                f.Release,
                f.StartScreening,
                f.EndScreening,
                f.AgeLimit,
                (FilmType)f.KindOfFilm,
                f.Content,
                f.Fees,
                f.Duration
            ));
    }
    static void UpdateSeasonsConf()
    {
        TypeAdapterConfig<UpdateSeriaDto, Seria>
            .NewConfig().ConstructUsing(s => new Seria(
                s.Num,
                s.IdFile
            ));
        
        TypeAdapterConfig<UpdateSeasonDto, Season>
            .NewConfig().ConstructUsing(s => new Season(
                s.Banner,
                s.Num
            ));
    }
    
}