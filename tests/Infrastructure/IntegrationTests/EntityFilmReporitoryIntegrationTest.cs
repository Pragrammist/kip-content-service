using Infrastructure.Repositories;
using Xunit;
using Infrastructure.Configuration;
using System.IO;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Dtos.CreateFilmDtos;
using System.Collections.Generic;
using System;
using FluentAssertions;

namespace IntegrationTests;

[Collection("MongoDb")]
public class EntityFilmReporitoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly EntityFilmRepositoryImpl _entityRepo;

    readonly FilmRepositoryImpl _repo;
    string RandomText => Path.GetRandomFileName();
    
    public EntityFilmReporitoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        MapsterConfiguration.ConfigureMapsterGlobally();
        _mongoFixture = mongoFixture;
        _entityRepo = new EntityFilmRepositoryImpl(_mongoFixture.FilmCollection);
        _repo = new FilmRepositoryImpl(_mongoFixture.FilmCollection);
    }

    [Fact]
    public async Task GetFilm()
    {   
        var film = await CreateFilmWithRandomName();

        var res = await _entityRepo.Get(film.Id);

        res.Should().NotBeNull();
    }
    async Task<FilmDto> CreateFilmWithRandomName()
    {
        var film = new CreateFilmDto 
        {
            AgeLimit = 3,
            Articles = new List<string>(),
            Banner = RandomText,
            Content = RandomText,
            Country = RandomText,
            Description = RandomText,
            Duration = TimeSpan.FromHours(1),
            EndScreening = DateTime.Today,
            Fees = 30,
            Genres = new List<string>(),
            Images = new List<string>(),
            KindOfFilm = 0,
            Name = RandomText,
            Nominations = new List<string>(),
            RelatedFilms = new List<string>(),
            Release = DateTime.Today,
            ReleaseType = 0,
            Seasons = new List<CreateSeasonDto>(),
            StartScreening = DateTime.Today,
            Stuff = new List<string>(),
            Tizers = new List<string>(),
            Trailers = new List<string>()
        };

        return await _repo.Create(film);
    }
}