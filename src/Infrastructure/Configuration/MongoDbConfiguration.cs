using MongoDB.Bson.Serialization;
using Core;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Infrastructure.Configuration;

public static class MongoDbConfiguration
{
    public static void ConfigureMongoDbGlobally()
    {
        BsonMemberMap SetStringId<T>(BsonClassMap<T> map) => map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)).SetIdGenerator(StringObjectIdGenerator.Instance);
        
        BsonClassMap.RegisterClassMap<Censor>(map =>
        {   
            map.AutoMap();
            SetStringId(map);
        });

        BsonClassMap.RegisterClassMap<Person>(map =>
        {
            map.AutoMap();
            SetStringId(map);
        });


        BsonClassMap.RegisterClassMap<Film>(map =>
        {
            map.AutoMap();
            SetStringId(map);
        });
    }

}
