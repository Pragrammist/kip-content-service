using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Core.Dtos;
using System.Collections.Generic;
using FluentAssertions;
using Core;
using MongoDB.Bson;

namespace IntegrationTests;

[Collection("MongoDb")]
public class FilmSelectionRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly FilmSelectionRepositoryImpl _repo;
    public FilmSelectionRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _repo = new FilmSelectionRepositoryImpl(_mongoFixture.FilmSelectionCollection, _mongoFixture.FilmCollection);
    } 
    
    [Fact]
    public async Task DeleteFilm()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();
        var filmdId = await CreateFilmWithRandomName();
        var filmIsAdded = await _repo.AddFilm(filmSelection.Id, filmdId);

        var res = await _repo.DeleteFilm(filmSelection.Id, filmdId);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddFilm()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();
        var filmId = await CreateFilmWithRandomName();

        var res = await _repo.AddFilm(filmSelection.Id, filmId);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddNotExistsFilm()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();
        
        var res = await _repo.AddFilm(filmSelection.Id, ObjectId.GenerateNewId().ToString());

        res.Should().BeFalse();
    }


    async Task<List<string>> ListOfFilm() => new List<string>{
        await CreateFilmWithRandomName(),
    };
    

    [Fact]
    public async Task Delete()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();

        var res = await _repo.Delete(filmSelection.Id);

        res.Should().Be(true);
    }
    [Fact]
    public async Task Get()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();

        var res = await _repo.Get(filmSelection.Id);
        
        res.Should().NotBeNull();
    }
    [Fact]
    public async Task GetMany()
    {
        var filmSelection  = await CreateFilmSelectionWithRandomName();

        var res = await _repo.Get();

        res.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ChangeName()
    {
        var filmSelection = await CreateFilmSelectionWithRandomName();
        var newName = "NEW NAME";

        var res = await _repo.ChangeName(filmSelection.Id, newName);

        res.Should().Be(true);
    }
    [Fact]
    public async Task Create()
    {
        var filmSelection = await CreateFilmSelectionWithRandomName();


        filmSelection.Should().NotBeNull();
    }

    async Task <FilmSelectionDto> CreateFilmSelectionWithRandomName()
    {
        var rName = Path.GetRandomFileName();
        var filmSelection = await _repo.Create(rName, films: new List<string>(){ 
            await CreateFilmWithRandomName(), 
            await CreateFilmWithRandomName(), 
            await CreateFilmWithRandomName()
        }) ?? throw new NullReferenceException("film selection is null in tests");
        return filmSelection;
    }
    async Task<string> CreateFilmWithRandomName()
    {
        Film film = new Film(RandomText, RandomText, RandomText, RandomText);

        await _mongoFixture.FilmCollection.InsertOneAsync(film);

        return film.Id;
        
    }
    string RandomText => Path.GetRandomFileName();
}