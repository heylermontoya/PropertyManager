using Newtonsoft.Json;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.property.Commands;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Net;

namespace PROPERTY_MANAGER.Api.Tests
{
    [TestFixture]
    public class PropertiesControllerTest
    {
        private const string PARAMETER_PATH = "api/Properties";

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
        public async Task ObtainListPropertiesAsync()
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
            List<PropertyDto>? listPropertyDto = JsonConvert.DeserializeObject<List<PropertyDto>>(data);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(listPropertyDto, Is.Not.Null);
                Assert.That(listPropertyDto, Is.Not.Empty);
            });
        }

        [Test]
        public async Task ObtainPropertyAsync()
        {
            //Arrange
            Guid propertyById = Guid.Parse("820C028D-B8C9-4641-BCF5-E59ECAF85C6B");

            HttpRequestMessage requestMessage = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/GetPropertyById/{propertyById}", UriKind.Relative)
            );

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();
            PropertyDto? propertyDto = JsonConvert.DeserializeObject<PropertyDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task CreateOwnerAsync_Ok()
        {
            //Arrange
            CreatePropertyCommand createPropertyCommand = new(
                "Modern New Apartment",
                "101 Ocean Dr, Miami Beach, FLZ",
                500000,
                "APT1010",
                2020,
                Guid.Parse("c87f84cf-8acf-4780-a75d-1f66cc3160b3")
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createPropertyCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            PropertyDto? propertyDto = JsonConvert.DeserializeObject<PropertyDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(propertyDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdateCustomerAsync_Ok()
        {
            //Arrange
            UpdatePropertyCommand updatePropertyCommand = new(
                Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"),
                "Modern New Apartment",
                "101 Ocean Dr, Miami Beach, FLZ",
                500000,
                "APT1010",
                2020,
                Guid.Parse("c87f84cf-8acf-4780-a75d-1f66cc3160b3")
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updatePropertyCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            PropertyDto? propertyDto = JsonConvert.DeserializeObject<PropertyDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdatePropertyPriceAsync_Ok()
        {
            //Arrange
            UpdatePropertyPriceCommand updatePropertyPriceCommand = new(
                Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"),
                500000
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Patch,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updatePropertyPriceCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            PropertyDto? propertyDto = JsonConvert.DeserializeObject<PropertyDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(propertyDto, Is.Not.Null);
            });
        }
    }
}
