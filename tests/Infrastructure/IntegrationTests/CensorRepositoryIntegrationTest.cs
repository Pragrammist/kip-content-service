using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Core.Dtos;
using System.Collections.Generic;
using FluentAssertions;
namespace IntegrationTests;


[Collection("MongoDb")]
public class CensorRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly CensorRepositoryImpl _repo;
    public CensorRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _repo = new CensorRepositoryImpl(_mongoFixture.CensorCollection);
    } 
    
    [Fact]
    public async Task DeleteFilm()
    {
        var censor  = await CreateFilmWithRandomName();
        var filmdId = "someid";
        await _repo.SetFilmsTop(censor.Id, new List<string>{filmdId});

        var res = await _repo.DeleteFilm(censor.Id, filmdId);

        res.Should().Be(true);
    }

    [Fact]
    public async Task SetFilmsTop()
    {
        var censor  = await CreateFilmWithRandomName();

        var res = await _repo.SetFilmsTop(censor.Id, new List<string>{"топ"});

        res.Should().Be(true);
    }
    [Fact]
    public async Task Delete()
    {
        var censor  = await CreateFilmWithRandomName();

        var res = await _repo.Delete(censor.Id);

        res.Should().Be(true);
    }
    [Fact]
    public async Task Get()
    {
        var censor  = await CreateFilmWithRandomName();

        var res = await _repo.Get(censor.Id);
        
        res.Should().NotBeNull();
    }
    [Fact]
    public async Task GetMany()
    {
        var censor  = await CreateFilmWithRandomName();

        var res = await _repo.Get();

        res.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ChangeName()
    {
        var censor = await CreateFilmWithRandomName();
        var newName = "NEW NAME";

        var res = await _repo.ChangeName(censor.Id, newName);

        res.Should().Be(true);
    }
    [Fact]
    public async Task Create()
    {
        var censor = await _repo.Create("somename", films: new List<string>(){ "фильм1", "фильм2", "фильм3" });


        censor.Should().NotBeNull();
    }

    async Task <CensorDto> CreateFilmWithRandomName()
    {
        var rName = Path.GetRandomFileName();
        var censor = await _repo.Create(rName, films: new List<string>(){ "фильм1", "фильм2", "фильм3" });
        return censor;
    }

}
