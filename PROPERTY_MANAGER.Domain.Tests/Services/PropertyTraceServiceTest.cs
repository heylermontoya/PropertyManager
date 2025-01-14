using NSubstitute;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;
using PROPERTY_MANAGER.Domain.Tests.DataBuilder;
using System.Globalization;

namespace PROPERTY_MANAGER.Domain.Tests.Services
{
    public class PropertyTraceServiceTest
    {
        private PropertyTraceService Service { get; set; } = default!;
        private IGenericRepository<PropertyTrace> Repository { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private PropertyTraceBuilder PropertyTraceBuilder { get; set; } = default!;

        [SetUp]
        public void Setup()
        {
            Repository = Substitute.For<IGenericRepository<PropertyTrace>>();
            QueryWrapper = Substitute.For<IQueryWrapper>();

            Service = new(
                Repository,
                QueryWrapper
            );

            PropertyTraceBuilder = new();
        }

        [TearDown]
        public void TearDown()
        {
            if (Repository is IDisposable disposableRepository)
            {
                disposableRepository.Dispose();
            }
        }

        [Test]
        public async Task CreatePropertyTraceAsync_Ok()
        {
            //Arrange
            DateTime dateSale = new(2022, 1, 15);
            string name = "Initial Sale";
            int value = 500000;
            int tax = 90;
            Guid idProperty = Guid.NewGuid();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithDateSale(dateSale)
                .WithName(name)
                .WithValue(value)
                .WithTax(tax)
                .WithIdProperty(idProperty)
                .Build();

            Repository.AddAsync(propertyTrace).ReturnsForAnyArgs(propertyTrace);

            //Act
            PropertyTrace result = await Service.CreatePropertyTraceAsync(dateSale, name, value, tax, idProperty);

            //Assert
            Assert.That(propertyTrace, Is.EqualTo(result));

            await Repository.ReceivedWithAnyArgs(1).AddAsync(
                Arg.Any<PropertyTrace>()
            );
        }

        [Test]
        public async Task UpdatePropertyTraceAsync_Ok()
        {
            //Arrange
            Guid idpropertyTrace = Guid.NewGuid();
            DateTime dateSale = new(2022, 1, 15);
            string name = "Initial Sale";
            int value = 500000;
            int tax = 90;
            Guid idProperty = Guid.NewGuid();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithDateSale(dateSale)
                .WithName(name)
                .WithValue(value)
                .WithTax(tax)
                .WithIdProperty(idProperty)
                .Build();

            Repository.GetByIdAsync(
                idpropertyTrace
            )
            .ReturnsForAnyArgs(propertyTrace);

            Repository.UpdateAsync(propertyTrace).ReturnsForAnyArgs(propertyTrace);

            //Act
            PropertyTrace result = await Service.UpdatePropertyTraceAsync(idpropertyTrace, dateSale, name, value, tax, idProperty);

            //Assert
            Assert.That(propertyTrace, Is.EqualTo(result));

            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).UpdateAsync(
                Arg.Any<PropertyTrace>()
            );
        }

        [Test]
        public async Task ObtainPropertyTraceByIdAsync_Ok()
        {
            //Arrange
            Guid idpropertyTrace = Guid.NewGuid();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithIdPropertyTrace(idpropertyTrace)
               .Build();

            Repository.GetByIdAsync(idpropertyTrace)
                .ReturnsForAnyArgs(propertyTrace);

            //Act
            PropertyTrace result = await Service.ObtainPropertyTraceByIdAsync(idpropertyTrace);

            //Assert
            Assert.That(propertyTrace, Is.EqualTo(result));
            Assert.That(propertyTrace.IdPropertyTrace, Is.EqualTo(result.IdPropertyTrace));
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainPropertyTraceByIdAsync_Failed()
        {
            //Arrange
            Guid idpropertyTrace = Guid.NewGuid();

            PropertyTrace? propertyTrace = null;

            Repository.GetByIdAsync(
                idpropertyTrace
            ).ReturnsForAnyArgs(propertyTrace);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.ObtainPropertyTraceByIdAsync(idpropertyTrace);
            });

            //Assert
            Assert.That(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.PropertyTraceNotFoundMessage,
                    idpropertyTrace
                ),
                Is.EqualTo(exception.Message)
            );

            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainListPropertyTraceAsync_WithFilter_Ok()
        {
            //Arrange
            string name = "John Smith";
            IEnumerable<FieldFilter>? fieldFilter = [ new(){
                Field = "Name",
                Value = name
            }];
            Guid idpropertyTrace = Guid.NewGuid();

            PropertyTrace propertyTrace = PropertyTraceBuilder
                .WithIdPropertyTrace(idpropertyTrace)
                .Build();

            List<PropertyTrace> listPropertyTrace = [propertyTrace];

            QueryWrapper
                .QueryAsync<PropertyTrace>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
                ).ReturnsForAnyArgs(listPropertyTrace);

            //Act
            List<PropertyTrace> result = await Service.ObtainListPropertyTraceAsync(fieldFilter);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(listPropertyTrace, Is.EqualTo(result));
                Assert.That(propertyTrace.IdPropertyTrace, Is.EqualTo(result[0].IdPropertyTrace));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<PropertyTrace>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
            );
        }
    }
}
