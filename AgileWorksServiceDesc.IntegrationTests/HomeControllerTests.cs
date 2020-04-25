using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AgileWorksServiceDesc.IntegrationTests
{
    [Collection("Sequential")]
    public class HomeControllerTests : TestBase
    {
        //private readonly HttpClient _client;
        public HomeControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {
            //_client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange
            var client = Factory.CreateClient();

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

    }
}
