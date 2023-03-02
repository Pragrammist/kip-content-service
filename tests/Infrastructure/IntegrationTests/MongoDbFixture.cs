using System;
using MongoDB.Driver;
using Core;
using Infrastructure.Configuration;

namespace IntegrationTests;

public class MongoDbFixture : IDisposable
{

    public IMongoClient Client { get; }
    public IMongoDatabase Db { get; }
    public const string DB_NAME = "kip-test-content-db";
    public const string CENSOR_COLECTION_NAME = "censors";
    public const string PERSON_COLECTION_NAME = "persons";
    public const string FILM_COLECTION_NAME = "films";

    public const string FILM_SELECTION_COLECTION_NAME = "selections";
    public IMongoCollection<Censor> CensorCollection { get; }

    public IMongoCollection<Person> PersonCollection { get; }

    public IMongoCollection<Film> FilmCollection { get; }

    public IMongoCollection<FilmSelection> FilmSelectionCollection { get; }

    public MongoDbFixture()
    {
        MongoDbConfiguration.ConfigureMongoDbGlobally();
        MapsterConfiguration.ConfigureMapsterGlobally();
        Client = new MongoClient("mongodb://localhost:27017");
        Db = Client.GetDatabase(DB_NAME);
        CensorCollection = Db.GetCollection<Censor>(CENSOR_COLECTION_NAME);
        PersonCollection = Db.GetCollection<Person>(PERSON_COLECTION_NAME);
        FilmCollection = Db.GetCollection<Film>(FILM_COLECTION_NAME);
        FilmSelectionCollection = Db.GetCollection<FilmSelection>(FILM_SELECTION_COLECTION_NAME);
    }

    public readonly InsertOneOptions _insOpt = new InsertOneOptions { };

    public readonly FilterDefinition<Censor> allFilter = Builders<Censor>.Filter.Empty;

    public void Dispose()
    {
        Client.DropDatabase(DB_NAME);
    }
    

}
