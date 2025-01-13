using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NSubstitute;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;
using PROPERTY_MANAGER.Domain.Services.property;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;
using PROPERTY_MANAGER.Domain.Tests.DataBuilder;

namespace PROPERTY_MANAGER.Domain.Tests.Services
{
    public class PropertyServiceTest
    {
        private PropertyService Service { get; set; } = default!;
        private OwnerService OwnerService { get; set; } = default!;
        private PropertyTraceService PropertyTraceService { get; set; } = default!;

        private IGenericRepository<Property> PropertyRepository { get; set; } = default!;
        private IGenericRepository<Owner> OwnerRepository { get; set; } = default!;
        private IGenericRepository<PropertyTrace> PropertyTraceRepository { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private IConfiguration Configuration { get; set; } = default!;
        private OwnerBuilder OwnerBuilder { get; set; } = default!;
        private PropertyBuilder PropertyBuilder { get; set; } = default!;
        private PropertyTraceBuilder PropertyTraceBuilder { get; set; } = default!;


        [SetUp]
        public void Setup()
        {
            PropertyRepository = Substitute.For<IGenericRepository<Property>>();
            OwnerRepository = Substitute.For<IGenericRepository<Owner>>();
            PropertyTraceRepository = Substitute.For<IGenericRepository<PropertyTrace>>();
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Configuration = Substitute.For<IConfiguration>();

            OwnerService = new(
                OwnerRepository,
                QueryWrapper
            );

            PropertyTraceService = new(
                PropertyTraceRepository,
                QueryWrapper
            );

            Service = new(
                PropertyRepository,
                QueryWrapper,
                OwnerService,
                PropertyTraceService,
                Configuration
            );

            OwnerBuilder = new();
            PropertyBuilder = new();
            PropertyTraceBuilder = new();
        }

        [TearDown]
        public void TearDown()
        {
            if (PropertyRepository is IDisposable disposablePropertyRepository)
            {
                disposablePropertyRepository.Dispose();
            }

            if (OwnerRepository is IDisposable disposableOwnerRepository)
            {
                disposableOwnerRepository.Dispose();
            }

            if (PropertyTraceRepository is IDisposable disposablePropertyTraceRepository)
            {
                disposablePropertyTraceRepository.Dispose();
            }
        }

        [Test]
        public async Task CreatePropertyAsync_Ok()
        {
            //Arrange
            string name = "Modern Apartment";
            string address = "101 Ocean Dr, Miami Beach, FL";
            int price = 500000;
            string codeInternal = "APT101";
            int year = 2020;
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .Build();

            Property property = PropertyBuilder
                .WithName(name)
                .WithAddress(address)
                .WithPrice(price)
                .WithCodeInternal(codeInternal)
                .WithYear(year)
                .WithIdOwner(idOwner)
                .Build();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .Build();

            Configuration["Tax"].Returns("19");

            OwnerRepository.GetByIdAsync(idOwner).ReturnsForAnyArgs(owner);
            PropertyRepository.AddAsync(property).ReturnsForAnyArgs(property);
            PropertyTraceRepository.AddAsync(propertyTrace).ReturnsForAnyArgs(propertyTrace);

            //Act
            Property result = await Service.CreatePropertyAsync(name, address, price, codeInternal, year, idOwner);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.EqualTo(result));
            });
            await OwnerRepository.ReceivedWithAnyArgs(1).GetByIdAsync(idOwner);
            await PropertyRepository.ReceivedWithAnyArgs(1).AddAsync(property);
            await PropertyTraceRepository.ReceivedWithAnyArgs(1).AddAsync(propertyTrace);
        }

        [Test]
        public async Task CreatePropertyAsync_Failed()
        {
            //Arrange
            string name = "Modern Apartment";
            string address = "101 Ocean Dr, Miami Beach, FL";
            int price = 500000;
            string codeInternal = "APT101";
            int year = 2020;
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .Build();

            Configuration["Tax"].Returns("hola");

            OwnerRepository.GetByIdAsync(idOwner).ReturnsForAnyArgs(owner);

            //Act            
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.CreatePropertyAsync(name, address, price, codeInternal, year, idOwner);
            });

            //Assert
            Assert.That(
                exception.Message,
                Is.EqualTo("Invalid Tax value in configuration.")
            );
            await OwnerRepository.ReceivedWithAnyArgs(1).GetByIdAsync(idOwner);
        }

        [Test]
        public async Task UpdatePropertyAsync_Ok()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();
            string name = "Modern Apartment";
            string address = "101 Ocean Dr, Miami Beach, FL";
            int price = 500000;
            string codeInternal = "APT101";
            int year = 2020;
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .WithName(name)
                .WithAddress(address)
                .WithPrice(price)
                .WithCodeInternal(codeInternal)
                .WithYear(year)
                .WithIdOwner(idOwner)
                .Build();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithIdProperty(idProperty)
                .Build();

            Configuration["Tax"].Returns("19");

            OwnerRepository.GetByIdAsync(idOwner).ReturnsForAnyArgs(owner);
            PropertyRepository.GetByIdAsync(idProperty).ReturnsForAnyArgs(property);
            PropertyRepository.UpdateAsync(property).ReturnsForAnyArgs(property);
            PropertyTraceRepository.AddAsync(propertyTrace).ReturnsForAnyArgs(propertyTrace);

            //Act
            Property result = await Service.UpdatePropertyAsync(idProperty, name, address, price, codeInternal, year, idOwner);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.EqualTo(result));
            });
            await OwnerRepository.ReceivedWithAnyArgs(1).GetByIdAsync(idOwner);
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(idProperty);
            await PropertyRepository.ReceivedWithAnyArgs(1).UpdateAsync(property);
            await PropertyTraceRepository.ReceivedWithAnyArgs(1).AddAsync(propertyTrace);
        }

        [Test]
        public async Task UpdatePropertyPriceAsync_Ok()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();
            string name = "Modern Apartment";
            string address = "101 Ocean Dr, Miami Beach, FL";
            int price = 500000;
            string codeInternal = "APT101";
            int year = 2020;
            Guid idOwner = Guid.NewGuid();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .WithName(name)
                .WithAddress(address)
                .WithPrice(price)
                .WithCodeInternal(codeInternal)
                .WithYear(year)
                .WithIdOwner(idOwner)
                .Build();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithIdProperty(idProperty)
                .Build();

            Configuration["Tax"].Returns("19");

            PropertyRepository.GetByIdAsync(idProperty).ReturnsForAnyArgs(property);
            PropertyRepository.UpdateAsync(property).ReturnsForAnyArgs(property);
            PropertyTraceRepository.AddAsync(propertyTrace).ReturnsForAnyArgs(propertyTrace);

            //Act
            Property result = await Service.UpdatePropertyPriceAsync(idProperty, price);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.EqualTo(result));
            });
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(idProperty);
            await PropertyRepository.ReceivedWithAnyArgs(1).UpdateAsync(property);
            await PropertyTraceRepository.ReceivedWithAnyArgs(1).AddAsync(propertyTrace);
        }

        [Test]
        public async Task ObtainPropertyByIdAsync_Ok()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();

            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            //Act
            Property result = await Service.ObtainPropertyByIdAsync(idProperty);

            //Assert
            Assert.That(property, Is.EqualTo(result));
            Assert.That(property.IdOwner, Is.EqualTo(result.IdOwner));
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainPropertyByIdAsync_Failed()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();

            Property? property = null;

            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.ObtainPropertyByIdAsync(idProperty);
            });

            //Assert
            Assert.That(
                $"The Property with id {idProperty} Not exist in the System",
                Is.EqualTo(exception.Message)
            );

            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainListPropertiesAsync_WithFilter_Ok()
        {
            //Arrange
            string name = "John Smith";
            IEnumerable<FieldFilter>? fieldFilter = [ new(){
                Field = "Name",
                Value = name
            }];
            Guid idProperty = Guid.NewGuid();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .WithName(name)
                .Build();

            List<Property> listProperty = [property];

            QueryWrapper
                .QueryAsync<Property>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
                ).ReturnsForAnyArgs(listProperty);

            //Act
            List<Property> result = await Service.ObtainListPropertiesAsync(fieldFilter);

            Assert.Multiple(() =>
            {
                //Assert
                Assert.That(listProperty, Is.EqualTo(result));
                Assert.That(property.IdOwner, Is.EqualTo(result[0].IdOwner));
                Assert.That(property.Name, Is.EqualTo(result[0].Name));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<Property>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
            );
        }
    }
}
