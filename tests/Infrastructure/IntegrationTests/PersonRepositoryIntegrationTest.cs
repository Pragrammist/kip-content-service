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

namespace IntegrationTests;

[Collection("MongoDb")]
public class PersonRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly PersonRepositoryImpl _repo;
    public PersonRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _repo = new PersonRepositoryImpl(_mongoFixture.PersonCollection);
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

        var res = await _repo.Delete(person.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task Get()
    {
        var person = await CreatePerson();
        
        var res = await _repo.Get(person.Id);

        res.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateBirthday()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Birthday = DateTime.MaxValue,
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateBirthPlace()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            BirthPlace = RandomText
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCareer()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Career = RandomText
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateHeight()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Height = 10
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateKindOfPerson()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            KindOfPerson = 1
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateName()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Name = RandomText
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdatePhoto()
    {
        var person = await CreatePerson();
        var updateData = new UpdatePersonDto{
            Photo = RandomText
        };

        var res = await _repo.UpdateData(person.Id, updateData);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task GetMany()
    {
        var person = await CreatePerson();
        
        var res = await _repo.Get();

        res.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AddFilm()
    {
        var person = await CreatePerson();

        var res = await _repo.AddFilm(person.Id, "someFilm");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteFilm()
    {
        var person = await CreatePerson();
        var filmId = "someFilm";
        await _repo.AddFilm(person.Id, filmId);

        var res = await _repo.DeleteFilm(person.Id, filmId);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddNomination()
    {
        var person = await CreatePerson();

        var res = await _repo.AddNomination(person.Id, "nomination");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var person = await CreatePerson();
        var nomination = "someNomination";
        await _repo.AddNomination(person.Id, nomination);

        var res = await _repo.DeleteNomination(person.Id, nomination);

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
        var res = await _repo.Create(p);
        return res;
    }
    string RandomText => Path.GetRandomFileName();
}
