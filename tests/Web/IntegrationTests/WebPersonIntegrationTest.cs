using System.Threading;
using Xunit;
using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection("WebContext")]
public class WebPersonIntegrationTest
{
    readonly WebFixture _webFixture;
    string RandomText => Path.GetRandomFileName();
    public WebPersonIntegrationTest (WebFixture webFixture)
    {
        _webFixture = webFixture;
    }

    [Fact]
    public async Task Create()
    {
        var person = new {
            KindOfPerson = 0,
            Birthday = "2023-03-02",
            Name = RandomText,
            Photo = RandomText,
            Height = 10,
            Career = RandomText,
            BirthPlace = RandomText
        };
        var res = await _webFixture.HttpClient.PostAsJsonAsync("person", person);
    
        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete()
    {
        var personId = await CreatePerson();

        var res = await _webFixture.HttpClient.DeleteAsync($"person/{personId}");
    
        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ChangeInfo()
    {
        var personId = await CreatePerson();
        
        var newInfo = new {
            Name = RandomText
        };

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"person/{personId}", newInfo);
    
        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOne()
    {
        var personId = await CreatePerson();

        var res = await _webFixture.HttpClient.GetAsync($"person/{personId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetMany()
    {
        var personId = await CreatePerson();

        var res = await _webFixture.HttpClient.GetAsync($"persons");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddNomination()
    {
        var personId = await CreatePerson();

        var nomination = "Лучший актер года";

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"person/nominations/{personId}/{nomination}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var personId = await CreatePerson();
        var nomination = "Лучший актер года";
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"person/nominations/{personId}/{nomination}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"person/nominations/delete/{personId}/{nomination}", new{});

        res.EnsureSuccessStatusCode();
    }

    async Task<string> CreatePerson()
    {
        var person = new {
            KindOfPerson = 0,
            Birthday = "2023-03-02",
            Name = RandomText,
            Photo = RandomText,
            Height = 10,
            Career = RandomText,
            BirthPlace = RandomText
        };
        var res = await _webFixture.HttpClient.PostAsJsonAsync("person", person);
    
        var json = await res.Content.ReadAsStringAsync();

        var id = JObject.Parse(json)["id"].ToString();

        return id;
    }
}
