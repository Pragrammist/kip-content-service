using Core;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Configuration;

public static class MongoDbExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, string connection, string dbName)
    {
        services.AddSingleton<IMongoClient>(p =>
        {
            var mongoClient = new MongoClient(connection);
            return mongoClient;
        });
        services.AddSingleton<IMongoDatabase>(p =>
        {
            var mongo = p.GetRequiredService<IMongoClient>();
            var db = mongo.GetDatabase(dbName);
            return db;
        });        
        
        return services;
    }
    public static IServiceCollection AddCensorMongoDbCollection(this IServiceCollection services, string mongoCollectionName)
    {
        services.AddSingleton<IMongoCollection<Censor>>(p =>
        {
            var db = p.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<Censor>(mongoCollectionName);
        });
        return services;
    }

    public static IServiceCollection AddPersonMongoDbCollection(this IServiceCollection services, string mongoCollectionName)
    {
        services.AddSingleton<IMongoCollection<Person>>(p =>
        {
            var db = p.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<Person>(mongoCollectionName);
        });
        return services;
    }

    public static IServiceCollection AddFilmMongoDbCollection(this IServiceCollection services, string mongoCollectionName)
    {
        services.AddSingleton<IMongoCollection<Film>>(p =>
        {
            var db = p.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<Film>(mongoCollectionName);
        });
        return services;
    }
    
    public static IServiceCollection AddFilmSelectionMongoDbCollection(this IServiceCollection services, string mongoCollectionName)
    {
        services.AddSingleton<IMongoCollection<FilmSelection>>(p =>
        {
            var db = p.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<FilmSelection>(mongoCollectionName);
        });
        return services;
    }
}
