using Xunit;

namespace IntegrationTests;

[CollectionDefinition("WebContext")]
public class WebFixtireCollection : ICollectionFixture<WebFixture>
{

}