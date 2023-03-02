using Xunit;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection("WebContext")]
public class WebSelectionIntegrationTest
{
    readonly WebFixture _webFixture;
    string RandomText => Path.GetRandomFileName();
    public WebSelectionIntegrationTest (WebFixture webFixture)
    {
        _webFixture = webFixture;
    }

    [Fact]
    public async Task Create()
    {
        var name = RandomText;

        var res = await _webFixture.HttpClient.PostAsJsonAsync($"selection/{name}", new string[0]);

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete()
    {
        var selectionId = await CreateSelection();

        var res = await _webFixture.HttpClient.DeleteAsync($"selection/{selectionId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOne()
    {
        var selectionId = await CreateSelection();

        var res = await _webFixture.HttpClient.GetAsync($"selection/{selectionId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetMany()
    {
        var selectionId = await CreateSelection();

        var res = await _webFixture.HttpClient.GetAsync($"selections");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddFilm()
    {
        var selectionId = await CreateSelection();
        var filmId = await CreateFilm();
        
        var resOfAddingList = await _webFixture.HttpClient.PutAsJsonAsync($"selection/films/{selectionId}/{filmId}", new{});

        resOfAddingList.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteFilm()
    {
        var selectionId = await CreateSelection();
        var filmId = await CreateFilm();
        var resOfAddingList = await _webFixture.HttpClient.PutAsJsonAsync($"selection/films/{selectionId}/{filmId}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"selection/films/delete/{selectionId}/{filmId}", new{});

        res.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task ChangeName()
    {
        var selectionId = await CreateSelection();
        var name = RandomText;
        
        var res = await _webFixture.HttpClient.PutAsJsonAsync($"selection/name/{selectionId}/{name}", new{});
        

        res.EnsureSuccessStatusCode();
    }

    async Task<string> CreateFilm()
    {
        var film = new 
        {
            AgeLimit = 18,
            Banner = "SOME BANNER",
            Name = "NAME",
            Description = "SOME DESC",
            Country = "COUNTRY",
            KindOfFilm = 0,
            ReleaseType = 0,
            Duration = "00:01:01",
            Release = "2023-02-25",
            StartScreening = "2023-02-25",
            EndScreening = "2023-02-25",
            Content = "CONTENT",
            Fees = 10,
            Images = new [] {"image"},
            Articles = new [] {"article"},
            Trailers = new [] {"trailer"},
            Tizers = new [] {"tizer"},
            Genres = new [] {"полное погружение"},
            Nominations = new [] {"оскар века"}, 
        };
        var res =  await _webFixture.HttpClient.PostAsJsonAsync("film", film);
        var jsonFilm = await res.Content.ReadAsStringAsync();
        var jobj = JObject.Parse(jsonFilm);
        var id = jobj["id"].ToString();
        return id;
    }

    public async Task<string> CreateSelection()
    {
        var name = RandomText;

        var res = await _webFixture.HttpClient.PostAsJsonAsync($"selection/{name}", new string[0]);

        var json = await res.Content.ReadAsStringAsync();

        var id = JObject.Parse(json)["id"].ToString();

        return id;
    }
}
