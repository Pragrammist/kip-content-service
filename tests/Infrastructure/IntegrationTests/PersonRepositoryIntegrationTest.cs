using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Core.Dtos;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos.UpdatePersonDtos;
using System.Collections.Generic;
using FluentAssertions;
using Core.Dtos.CreateFilmDtos;
using Infrastructure.Configuration;

namespace IntegrationTests;

[Collection("MongoDb")]
public class PersonRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly PersonRepositoryImpl _personRepo;
    readonly FilmRepositoryImpl _filmRepo;

    public PersonRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _personRepo = new PersonRepositoryImpl(_mongoFixture.PersonCollection, _mongoFixture.FilmCollection);
        _filmRepo = new FilmRepositoryImpl(_mongoFixture.FilmCollection, _mongoFixture.PersonCollection, _mongoFixture.CensorCollection, _mongoFixture.FilmSelectionCollection);
    } 

    [Fact]
    public async Task Create()
    {
        var res = await CreatePerson();
        
        res.Should().NotBeNull();
    }

    [Fact]
    public async Task Delete()
    {
        var person = await CreatePerson();

        var personIsDeleted = await _personRepo.Delete(person.Id);

        personIsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCheckFilm()
    {
        var person = await CreatePerson();
        CreateFilmDto createFilmDto = new CreateFilmDto {
            AgeLimit = 12,
            Banner = RandomText,
            Content = RandomText,
            Country = RandomText,
            Description = RandomText,
            Duration = TimeSpan.Zero,
            Fees = 1232,
            Name = RandomText
        };
        var addedFilmDto = await _filmRepo.Create(createFilmDto);
        var filmIsAdded = await _filmRepo.AddPerson(addedFilmDto.Id, person.Id);

        var personIsDeleted = await _personRepo.Delete(person.Id);

        var updatedFilmDto = await _filmRepo.Get(addedFilmDto.Id) ?? throw new NullReferenceException("film is not found when it get after update");
        var personIdIsDeleted = !updatedFilmDto.Stuff.Contains(person.Id);
        personIdIsDeleted.Should().BeTrue();
        personIsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task Get()
    {
        var person = await CreatePerson();
        
        var res = await _personRepo.Get(person.Id);

        res.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateBirthday()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Birthday = DateTime.MaxValue,
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateBirthPlace()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            BirthPlace = RandomText
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCareer()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Career = RandomText
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateHeight()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Height = 10
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateKindOfPerson()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            KindOfPerson = 1
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateName()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Name = RandomText
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdatePhoto()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Photo = RandomText
        };

        var res = await _personRepo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task GetMany()
    {
        var person = await CreatePerson();
        
        var res = await _personRepo.Get();

        res.Count().Should().BeGreaterThan(0);
    }


    [Fact]
    public async Task AddNomination()
    {
        var person = await CreatePerson();

        var res = await _personRepo.AddNomination(person.Id, "nomination");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var person = await CreatePerson();
        var nomination = "someNomination";
        await _personRepo.AddNomination(person.Id, nomination);

        var res = await _personRepo.DeleteNomination(person.Id, nomination);

        res.Should().BeTrue();
    }

    async Task <PersonDto> CreatePerson()
    {
        CreatePersonDto p = new CreatePersonDto{
            Birthday = default,
            BirthPlace = RandomText,
            Career =  RandomText,
            Films = new List<string>() {RandomText, RandomText, RandomText},
            Height = 30,
            KindOfPerson = 0,
            Name = RandomText,
            Nominations = new List<string>() {RandomText, RandomText, RandomText},
            Photo = RandomText
        };
        var res = await _personRepo.Create(p);
        return res;
    }
    string RandomText => Path.GetRandomFileName();
}
