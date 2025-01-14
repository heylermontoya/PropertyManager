using Microsoft.Extensions.Configuration;
using NSubstitute;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;
using PROPERTY_MANAGER.Domain.Services.property;
using PROPERTY_MANAGER.Domain.Services.propertyImage;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;
using PROPERTY_MANAGER.Domain.Tests.DataBuilder;
using System.Globalization;
using System.Linq.Expressions;

namespace PROPERTY_MANAGER.Domain.Tests.Services
{
    public class PropertyImageServiceTest
    {
        private PropertyImageService Service { get; set; } = default!;
        private PropertyService PropertyService { get; set; } = default!;
        private OwnerService OwnerService { get; set; } = default!;
        private PropertyTraceService PropertyTraceService { get; set; } = default!;
        private IGenericRepository<PropertyImage> Repository { get; set; } = default!;
        private IGenericRepository<Property> PropertyRepository { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;

        private IConfiguration Configuration { get; set; } = default!;
        private PropertyImageBuilder PropertyImageBuilder { get; set; } = default!;
        private PropertyBuilder PropertyBuilder { get; set; } = default!;


        [SetUp]
        public void Setup()
        {
            Repository = Substitute.For<IGenericRepository<PropertyImage>>();
            PropertyRepository = Substitute.For<IGenericRepository<Property>>();
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Configuration = Substitute.For<IConfiguration>();

            PropertyService = new(
                PropertyRepository,
                QueryWrapper,
                OwnerService,
                PropertyTraceService,
                Configuration
            );

            Service = new(
                Repository,
                QueryWrapper,
                PropertyService,
                Configuration
            );

            PropertyImageBuilder = new();
            PropertyBuilder = new();
        }

        [TearDown]
        public void TearDown()
        {
            if (Repository is IDisposable disposableRepository)
            {
                disposableRepository.Dispose();
            }

            if (PropertyRepository is IDisposable disposablePropertyRepository)
            {
                disposablePropertyRepository.Dispose();
            }
        }

        [Test]
        public async Task CreatePropertyImageAsync_Ok()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();

            Repository.AddAsync(propertyImage).ReturnsForAnyArgs(propertyImage);

            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            Configuration["MaxPropertyFiles"].Returns("3");

            //Act
            PropertyImage result = await Service.CreatePropertyImageAsync(idProperty, file, true);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(propertyImage, Is.EqualTo(result));
            });
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).AddAsync(
                Arg.Any<PropertyImage>()
            );
        }

        [Test]
        public async Task CreatePropertyImageAsync_NumberFilesInvalid_Failed()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";
            string maxPropertyFiles = "0";

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();


            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            Repository.GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            ).ReturnsForAnyArgs([propertyImage]);

            Configuration["MaxPropertyFiles"].Returns(maxPropertyFiles);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.CreatePropertyImageAsync(idProperty, file, true);
            });

            //Assert
            Assert.That(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.MaxPropertyFilesMessage,
                    maxPropertyFiles
                ),
                Is.EqualTo(exception.Message)
            );
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            );
        }
        
        [Test]
        public async Task CreatePropertyImageAsync_GetMaxPropertyFilesInvalid_Failed()
        {
            //Arrange
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";
            string maxPropertyFiles = "ho";

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();


            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            Repository.GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            ).ReturnsForAnyArgs([propertyImage]);

            Configuration["MaxPropertyFiles"].Returns(maxPropertyFiles);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.CreatePropertyImageAsync(idProperty, file, true);
            });

            //Assert
            Assert.That(
                MessagesExceptions.MaxPropertyFilesInvalidMessage,
                Is.EqualTo(exception.Message)
            );
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            );
        }

        [Test]
        public async Task UpdatePropertyImageAsync_Ok()
        {
            //Arrange
            Guid idPropertyImage = Guid.NewGuid();
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";
            bool enabled = true;

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();

            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            Repository.GetByIdAsync(
                idPropertyImage
            ).ReturnsForAnyArgs(propertyImage);

            Repository.UpdateAsync(propertyImage).ReturnsForAnyArgs(propertyImage);

            Configuration["MaxPropertyFiles"].Returns("3");

            //Act
            PropertyImage result = await Service.UpdatePropertyImageAsync(idPropertyImage, idProperty, file, enabled);

            //Assert
            Assert.That(propertyImage, Is.EqualTo(result));

            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).UpdateAsync(
                Arg.Any<PropertyImage>()
            );
        }
        
        [Test]
        public async Task UpdatePropertyImageAsync_NumberFilesInvalid_Failed()
        {
            //Arrange
            Guid idPropertyImage = Guid.NewGuid();
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";
            bool enabled = true;
            string maxPropertyFiles = "0";

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Property property = PropertyBuilder
                .WithIdProperty(idProperty)
                .Build();

            PropertyRepository.GetByIdAsync(
                idProperty
            ).ReturnsForAnyArgs(property);

            Repository.GetByIdAsync(
                idPropertyImage
            ).ReturnsForAnyArgs(propertyImage);

            Repository.GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            ).ReturnsForAnyArgs([]);

            Configuration["MaxPropertyFiles"].Returns(maxPropertyFiles);

            //Act            
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.UpdatePropertyImageAsync(idPropertyImage, idProperty, file, enabled);
            });

            //Assert
            Assert.That(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.MaxPropertyFilesMessage,
                    maxPropertyFiles
                ),
                Is.EqualTo(exception.Message)
            );
            await PropertyRepository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetAsync(
                    Arg.Any<Expression<Func<PropertyImage, bool>>?>()
            );
        }

        [Test]
        public async Task ObtainPropertyImageByIdAsync_Ok()
        {
            //Arrange
            Guid idPropertyImage = Guid.NewGuid();
            Guid idProperty = Guid.NewGuid();
            string file = "path/file1.png";

            PropertyImage propertyImage = PropertyImageBuilder
                .WithIdProperty(idProperty)
                .WithFile(file)
                .Build();

            Repository.GetByIdAsync(
                idPropertyImage
            ).ReturnsForAnyArgs(propertyImage);

            //Act
            PropertyImage result = await Service.ObtainPropertyImageByIdAsync(idPropertyImage);

            //Assert
            Assert.That(propertyImage, Is.EqualTo(result));
            Assert.That(propertyImage.IdPropertyImage, Is.EqualTo(result.IdPropertyImage));
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainPropertyImageByIdAsync_Failed()
        {
            //Arrange
            Guid idPropertyImage = Guid.NewGuid();

            PropertyImage? propertyImage = null;

            Repository.GetByIdAsync(
                idPropertyImage
            ).ReturnsForAnyArgs(propertyImage);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.ObtainPropertyImageByIdAsync(idPropertyImage);
            });

            //Assert
            Assert.That(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.PropertyImageNotFoundByIdMessage,
                    idPropertyImage
                ),
                Is.EqualTo(exception.Message)
            );

            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainListPropertyImagesAsync_WithoutFilter_Ok()
        {
            //Arrange
            IEnumerable<FieldFilter> fieldFilter = [];

            PropertyImage propertyImage = PropertyImageBuilder
                .Build();

            List<PropertyImage> listPropertyImage = [propertyImage];

            QueryWrapper
                .QueryAsync<PropertyImage>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, [])]
                ).ReturnsForAnyArgs(listPropertyImage);

            //Act
            List<PropertyImage> result = await Service.ObtainListPropertyImageAsync(fieldFilter);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(listPropertyImage, Is.EqualTo(result));
                Assert.That(propertyImage.IdPropertyImage, Is.EqualTo(result[0].IdPropertyImage));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<PropertyImage>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, [])]
            );
        }

        [Test]
        public async Task ObtainListPropertyImagesAsync_WithFilter_Ok()
        {
            //Arrange
            string name = "John Smith";
            IEnumerable<FieldFilter>? fieldFilter = [ new(){
                Field = "Name",
                Value = name
            }];
            Guid idPropertyImage = Guid.NewGuid();

            PropertyImage propertyImage = PropertyImageBuilder
                .Build();

            List<PropertyImage> listPropertyImage = [propertyImage];

            QueryWrapper
                .QueryAsync<PropertyImage>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
                ).ReturnsForAnyArgs(listPropertyImage);

            //Act
            List<PropertyImage> result = await Service.ObtainListPropertyImageAsync(fieldFilter);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(listPropertyImage, Is.EqualTo(result));
                Assert.That(propertyImage.IdPropertyImage, Is.EqualTo(result[0].IdPropertyImage));
                Assert.That(propertyImage.File, Is.EqualTo(result[0].File));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<PropertyImage>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
            );
        }
    }
}
