using Newtonsoft.Json;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.owner.Commands;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Net;

namespace PROPERTY_MANAGER.Api.Tests
{
    [TestFixture]
    public class OwnerControllerTest
    {
        private const string PARAMETER_PATH = "api/Owner";

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
        public async Task ObtainListOwnersAsync()
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
            List<OwnerDto>? listOwnerDto = JsonConvert.DeserializeObject<List<OwnerDto>>(data);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(listOwnerDto, Is.Not.Null);
                Assert.That(listOwnerDto, Is.Not.Empty);
            });
        }

        [Test]
        public async Task ObtainOwnerAsync_Ok()
        {
            //Arrange
            Guid ownerId = Guid.Parse("1010000C-9F47-43B5-AE47-738CFA8A60AA");

            HttpRequestMessage requestMessage = new(
                HttpMethod.Get,
                new Uri($"{PARAMETER_PATH}/GetOwnerById/{ownerId}", UriKind.Relative)
            );

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();

            string data = await result.Content.ReadAsStringAsync();
            OwnerDto? ownerDto = JsonConvert.DeserializeObject<OwnerDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(ownerDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task CreateOwnerAsync_Ok()
        {
            //Arrange
            CreateOwnerCommand createOwnerCommand = new(
                "John Montoya",
                 "123 Main St, Florida",
                 "https://example.com/john_smith.png",
                 new DateTime(1980, 5, 15)
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Post,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(createOwnerCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            OwnerDto? ownerDto = JsonConvert.DeserializeObject<OwnerDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(ownerDto, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdateCustomerAsync_Ok()
        {
            //Arrange
            UpdateOwnerCommand updateOwnerCommand = new(
                Guid.Parse("c87f84cf-8acf-4780-a75d-1f66cc3160b3"),
                "John Orjuela",
                 "123 Main St, New York",
                 "https://example.com/john_smith.png",
                 new DateTime(1980, 5, 15)
            );

            HttpRequestMessage requestMessage = new(
                HttpMethod.Put,
                new Uri($"{PARAMETER_PATH}", UriKind.Relative)
            )
            {
                Content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(updateOwnerCommand),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };

            //Act
            HttpResponseMessage result = await httpClient!.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
            string data = await result.Content.ReadAsStringAsync();
            OwnerDto? ownerDto = JsonConvert.DeserializeObject<OwnerDto>(data);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(ownerDto, Is.Not.Null);
            });
        }
    }
}
