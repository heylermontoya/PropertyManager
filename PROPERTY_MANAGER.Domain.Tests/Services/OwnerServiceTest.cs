using NSubstitute;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;
using PROPERTY_MANAGER.Domain.Tests.DataBuilder;
using System.Globalization;
using System.Linq.Expressions;

namespace PROPERTY_MANAGER.Domain.Tests.Services
{
    public class OwnerServiceTest
    {
        private OwnerService Service { get; set; } = default!;
        private IGenericRepository<Owner> Repository { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private OwnerBuilder OwnerBuilder { get; set; } = default!;

        [SetUp]
        public void Setup()
        {
            Repository = Substitute.For<IGenericRepository<Owner>>();
            QueryWrapper = Substitute.For<IQueryWrapper>();

            Service = new(
                Repository,
                QueryWrapper
            );

            OwnerBuilder = new();
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
        public async Task CreateOwnerAsync_Ok()
        {
            //Arrange
            string name = "John Smith";
            string address = "123 Main St, Los Angeles, CA";
            string photo = "https://example.com/john_smith.png";
            DateTime birthday = new(1980, 5, 15);

            Owner owner = OwnerBuilder
                .WithName(name)
                .WithAddress(address)
                .WithPhoto(photo)
                .WithBirthday(birthday)
                .Build();

            Repository.AddAsync(owner).ReturnsForAnyArgs(owner);

            Repository.GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            ).ReturnsForAnyArgs([]);

            //Act
            Owner result = await Service.CreateOwnerAsync(name, address, photo, birthday);

            //Assert
            Assert.That(owner, Is.EqualTo(result));

            await Repository.ReceivedWithAnyArgs(1).AddAsync(
                Arg.Any<Owner>()
            );
            await Repository.ReceivedWithAnyArgs(2).GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            );
        }

        [Test]
        public async Task CreateOwnerAsync_Failed()
        {
            //Arrange
            string name = "John Smith";
            string address = "123 Main St, Los Angeles, CA";
            string photo = "https://example.com/john_smith.png";
            DateTime birthday = new(1980, 5, 15);

            Owner owner = OwnerBuilder
                .WithName(name)
                .WithAddress(address)
                .WithPhoto(photo)
                .WithBirthday(birthday)
                .Build();

            List<Owner> listOwner = [OwnerBuilder.Build()];

            Repository.AddAsync(owner).ReturnsForAnyArgs(owner);

            Repository.GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            ).ReturnsForAnyArgs(listOwner);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.CreateOwnerAsync(name, address, photo, birthday);
            });

            //Assert
            Assert.That(
                exception.Message,
                Is.EqualTo(MessagesExceptions.NameAlreadyExistsMessage)
            );

            await Repository.ReceivedWithAnyArgs(1).GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            );
        }

        [Test]
        public async Task UpdateOwnerAsync_Ok()
        {
            //Arrange
            Guid idOwner = Guid.NewGuid();
            string name = "John Smith";
            string address = "123 Main St, Los Angeles, CA";
            string photo = "https://example.com/john_smith.png";
            DateTime birthday = new(1980, 5, 15);

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .WithName(name)
                .WithAddress(address)
                .WithPhoto(photo)
                .WithBirthday(birthday)
                .Build();

            Repository.GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            ).ReturnsForAnyArgs([]);

            Repository.GetByIdAsync(
                idOwner
            ).ReturnsForAnyArgs(owner);

            Repository.UpdateAsync(owner).ReturnsForAnyArgs(owner);

            //Act
            Owner result = await Service.UpdateOwnerAsync(idOwner, name, address, photo, birthday);

            //Assert
            Assert.That(owner, Is.EqualTo(result));

            await Repository.ReceivedWithAnyArgs(2).GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            );
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
            await Repository.ReceivedWithAnyArgs(1).UpdateAsync(
                Arg.Any<Owner>()
            );
        }

        [Test]
        public async Task UpdateOwnerAsync_Failed()
        {
            //Arrange
            Guid idOwner = Guid.NewGuid();
            string name = "John Smith";
            string address = "123 Main St, Los Angeles, CA";
            string photo = "https://example.com/john_smith.png";
            DateTime birthday = new(1980, 5, 15);

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .WithName(name)
                .WithAddress(address)
                .WithPhoto(photo)
                .WithBirthday(birthday)
                .Build();

            List<Owner> listOwner = [OwnerBuilder.Build()];

            Repository.AddAsync(owner).ReturnsForAnyArgs(owner);

            Repository.GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            ).ReturnsForAnyArgs(listOwner);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.UpdateOwnerAsync(idOwner, name, address, photo, birthday);
            });

            //Assert
            Assert.That(
                exception.Message,
                Is.EqualTo(MessagesExceptions.NameAlreadyExistsMessage)
            );

            await Repository.ReceivedWithAnyArgs(1).GetAsync(
                Arg.Any<Expression<Func<Owner, bool>>?>()
            );
        }

        [Test]
        public async Task ObtainOwnerByIdAsync_Ok()
        {
            //Arrange
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .Build();

            Repository.GetByIdAsync(
                idOwner
            ).ReturnsForAnyArgs(owner);

            //Act
            Owner result = await Service.ObtainOwnerByIdAsync(idOwner);

            //Assert
            Assert.That(owner, Is.EqualTo(result));
            Assert.That(owner.IdOwner, Is.EqualTo(result.IdOwner));
            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainOwnerByIdAsync_Failed()
        {
            //Arrange
            Guid idOwner = Guid.NewGuid();

            Owner? owner = null;

            Repository.GetByIdAsync(
                idOwner
            ).ReturnsForAnyArgs(owner);

            //Act
            AppException exception = Assert.ThrowsAsync<AppException>(async () =>
            {
                await Service.ObtainOwnerByIdAsync(idOwner);
            });

            //Assert
            Assert.That(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.OwnerNotFoundMessage,
                    idOwner
                ),
                Is.EqualTo(exception.Message)
            );

            await Repository.ReceivedWithAnyArgs(1).GetByIdAsync(
                Arg.Any<Guid>()
            );
        }

        [Test]
        public async Task ObtainListOwnersAsync_WithoutFilter_Ok()
        {
            //Arrange
            IEnumerable<FieldFilter> fieldFilter = [];
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .Build();

            List<Owner> listOwner = [owner];

            QueryWrapper
                .QueryAsync<Owner>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, [])]
                ).ReturnsForAnyArgs(listOwner);

            //Act
            List<Owner> result = await Service.ObtainListOwnersAsync(fieldFilter);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(listOwner, Is.EqualTo(result));
                Assert.That(owner.IdOwner, Is.EqualTo(result[0].IdOwner));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<Owner>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, [])]
            );
        }

        [Test]
        public async Task ObtainListOwnersAsync_WithFilter_Ok()
        {
            //Arrange
            string name = "John Smith";
            IEnumerable<FieldFilter>? fieldFilter = [ new(){
                Field = "Name",
                Value = name
            }];
            Guid idOwner = Guid.NewGuid();

            Owner owner = OwnerBuilder
                .WithIdOwner(idOwner)
                .WithName(name)
                .Build();

            List<Owner> listOwner = [owner];

            QueryWrapper
                .QueryAsync<Owner>(
                    ItemsMessageConstants.GetProperties
                        .GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
                ).ReturnsForAnyArgs(listOwner);

            //Act
            List<Owner> result = await Service.ObtainListOwnersAsync(fieldFilter);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(listOwner, Is.EqualTo(result));
                Assert.That(owner.IdOwner, Is.EqualTo(result[0].IdOwner));
                Assert.That(owner.Name, Is.EqualTo(result[0].Name));
            });
            await QueryWrapper.ReceivedWithAnyArgs(1).QueryAsync<Owner>(
                ItemsMessageConstants.GetProperties.GetDescription(),
                new { },
                [FieldFilterHelper.BuildQuery(addWhereClause: true, fieldFilter)]
            );
        }
    }
}