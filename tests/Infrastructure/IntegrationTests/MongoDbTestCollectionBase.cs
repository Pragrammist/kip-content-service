using Xunit;

namespace IntegrationTests;

[CollectionDefinition("MongoDb")]
public class MongoDbTestCollectionBase : ICollectionFixture<MongoDbFixture>
{

}