using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Exceptions;

namespace PROPERTY_MANAGER.Domain.Tests.Entities
{
    public class PropertyTest
    {
        [Test]
        public void SetIdOwner_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.IdOwner = Guid.Empty;
            });

            //Assert
            Assert.That(
                MessagesExceptions.GuidNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetIdProperty_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.IdProperty = Guid.Empty;
            });

            //Assert
            Assert.That(
                MessagesExceptions.GuidNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetName_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.Name = "Ho";
            });

            //Assert
            Assert.That(
                MessagesExceptions.SizeFieldNameNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetName_Ok()
        {
            //Arrange
            string name = "Hol";
            Property property = new();

            //Act & Assert
            property.Name = name;

            //Assert
            Assert.That(
                name,
                Is.EqualTo(property.Name)
            );
        }

        [Test]
        public void SetAddress_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.Address = "Ho";
            });

            //Assert
            Assert.That(
                MessagesExceptions.SizeFieldAddressNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetAddress_Ok()
        {
            //Arrange
            string address = "holaer";
            Property property = new();

            //Act & Assert            
            property.Address = address;

            //Assert
            Assert.That(
                address,
                Is.EqualTo(property.Address)
            );
        }

        [Test]
        public void SetPrice_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.Price = 0;
            });

            //Assert
            Assert.That(
                MessagesExceptions.PriceNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetPrice_Ok()
        {
            //Arrange
            int price = 1;
            Property property = new();

            //Act & Assert            
            property.Price = price;

            //Assert
            Assert.That(
                price,
                Is.EqualTo(property.Price)
            );
        }

        [Test]
        public void SetCodeInternal_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.CodeInternal = "Ho";
            });

            //Assert
            Assert.That(
                MessagesExceptions.SizeFieldCodeInternalNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetCodeInternal_Ok()
        {
            //Arrange
            string codeInternal = "holaer";
            Property property = new();

            //Act & Assert            
            property.CodeInternal = codeInternal;

            //Assert
            Assert.That(
                codeInternal,
                Is.EqualTo(property.CodeInternal)
            );
        }

        [Test]
        public void SetYear_Failed()
        {
            //Arrange
            Property property = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                property.Year = -1;
            });

            //Assert
            Assert.That(
                MessagesExceptions.YearNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetYear_Ok()
        {
            //Arrange
            int year = 1900;
            Property property = new();

            //Act & Assert            
            property.Year = year;

            //Assert
            Assert.That(
                year,
                Is.EqualTo(property.Year)
            );
        }
    }
}
