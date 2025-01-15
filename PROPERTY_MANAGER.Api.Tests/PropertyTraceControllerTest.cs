using Newtonsoft.Json;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Net;

namespace PROPERTY_MANAGER.Api.Tests
{
    [TestFixture]
    public class PropertyTraceControllerTest
    {
        private const string PARAMETER_PATH = "api/PropertyTrace";

        private TestStartup<Program>? factory;
        private HttpClient? httpClient;

        [SetUp]
        public void SetUp()
        {
            factory = new TestStartup<Program>();
            httpClient = factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(5);
        }

        [TearDown]
        public void TearDown()
        {
            if (factory is IDisposable disposableFactory)
            {
                disposableFactory.Dispose();
            }
            if (httpClient is IDisposable disposableHttpClient)
            {
                disposableHttpClient.Dispose();
            }
        }

        [Test]
        public async Task ObtainListPropertiesTraceAsync_Ok()
        {
            // Arrange
            List<FieldFilter> fieldFilters = [];

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}/list", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(fieldFilters),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            // Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);
            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();
            List<PropertyTraceDto>? propertyTraceDto = JsonConvert.DeserializeObject<List<PropertyTraceDto>>(data);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyTraceDto, Is.Not.Null);
                Assert.That(propertyTraceDto, Is.Not.Empty);
            });
        }

        [Test]
        public async Task ObtainPropertyTraceAsync_Ok()
        {
            //Arrange
            Guid propertyTraceId = Guid.Parse("1472ae86-ac58-4042-b16e-f57858c82c5c");

            HttpRequestMessage requestMessage = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/GetPropertyTraceById/{propertyTraceId}", UriKind.Relative)
            );

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();
            PropertyTraceDto? propertyTraceDto = JsonConvert.DeserializeObject<PropertyTraceDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyTraceDto, Is.Not.Null);
            });
        }
    }
}
