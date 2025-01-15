using Newtonsoft.Json;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.propertyImage.Commands;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Net;

namespace PROPERTY_MANAGER.Api.Tests
{
    [TestFixture]
    public class PropertyImageControllerTest
    {
        private const string PARAMETER_PATH = "api/PropertyImage";

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
        public async Task ObtainListPropertiesImagesAsync()
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
            List<PropertyImageDto>? listOwnerDto = JsonConvert.DeserializeObject<List<PropertyImageDto>>(data);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(listOwnerDto, Is.Not.Null);
                Assert.That(listOwnerDto, Is.Not.Empty);
            });
        }

        [Test]
        public async Task ObtainPropertyImageAsync()
        {
            //Arrange
            Guid propertyImageId = Guid.Parse("aae41333-5511-4231-9e44-2cbc33c95153");

            HttpRequestMessage requestMessage = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/GetPropertyImageById/{propertyImageId}", UriKind.Relative)
            );

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();
            PropertyImageDto? PropertyImageDto = JsonConvert.DeserializeObject<PropertyImageDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(PropertyImageDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task CreatePropertyImageAsync_Ok()
        {
            //Arrange
            CreatePropertyImageCommand createPropertyImageCommand = new(
                Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"),
                 "https://example.com/john_smith.png"
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createPropertyImageCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            PropertyImageDto? propertyImageDto = JsonConvert.DeserializeObject<PropertyImageDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(propertyImageDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdatePropertyImageAsync_Ok()
        {
            //Arrange
            UpdatePropertyImageCommand updatePropertyImageCommand = new(
                Guid.Parse("aae41333-5511-4231-9e44-2cbc33c95153"),
                Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"),
                 "https://example.com/john_smith.png",
                 true
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updatePropertyImageCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            PropertyImageDto? propertyImageDto = JsonConvert.DeserializeObject<PropertyImageDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyImageDto, Is.Not.Null);
            });
        }
    }
}
