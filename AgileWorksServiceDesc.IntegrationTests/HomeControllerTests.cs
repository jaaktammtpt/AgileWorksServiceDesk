using AngleSharp.Html.Dom;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AgileWorksServiceDesc.IntegrationTests
{
    [Collection("Sequential")]
    public class HomeControllerTests : TestBase
    {
        private readonly HttpClient client;
        public HomeControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {
            //Arrange
            client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Home/Edit/1")]
        [InlineData("/Home/Create")]
        public async Task Get_EndPointsReturnsSuccessForClient(string url)
        {
            //Arrange

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Create_WhenCalled_ReturnsCreateForm()
        {
            //Arrange

            //Act
            var response = await client.GetAsync("/Home/Create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("Please enter new service request data", responseString);
        }

        [Fact]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            //Arrange
            var initResponse = await client.GetAsync("/Home/Create");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/Create");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "DueDateTime", "2022-04-27 12:15" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            //Act
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("The Description field is required.", responseString);
        }

        [Fact]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithRequest()
        {
            //Arrange
            var initResponse = await client.GetAsync("/Home/Create");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/Create");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "Description", "This is a description" },
                { "DueDateTime", "2022-04-27 12:15" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            //Act
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Update request", responseString);
            Assert.Contains("This is a description", responseString);
            Assert.Contains("27.04.2022 12:15", responseString);
        }

        [Fact]
        public async Task Edit_WhenPOSTExecuted_ReturnsToIndexViewWithRequest()
        {
            //Arrange
            var initResponse = await client.GetAsync("/Home/Edit/1");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/Edit/1");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "Description", "This is a edited description" },
                { "DueDateTime", "2023-04-27 12:15" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            //Act
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Update request", responseString);
            Assert.Contains("This is a edited description", responseString);
            Assert.Contains("27.04.2023 12:15", responseString);
        }

        [Fact]
        public async Task Edit_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            //Arrange
            var initResponse = await client.GetAsync("/Home/Edit/2");
            var (fieldValue, cookieValue) = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Home/Edit/2");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryFieldName, fieldValue },
                { "Description", "" },
                { "DueDateTime", "2023-04-27 12:15" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            //Act
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("The Description field is required.", responseString);
        }

        [Fact]
        public async Task Edit_WhenCalled_ReturnsCreateForm()
        {
            //Arrange

            //Act
            var response = await client.GetAsync("/Home/Edit/3");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Contains("Please update service request data", responseString);
        }
    }
}
