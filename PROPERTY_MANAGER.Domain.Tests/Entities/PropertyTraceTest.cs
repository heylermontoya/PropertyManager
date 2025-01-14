using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Exceptions;

namespace PROPERTY_MANAGER.Domain.Tests.Entities
{
    public class PropertyTraceTest
    {
        [Test]
        public void SetIdPropertyTrace_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.IdPropertyTrace = Guid.Empty;
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
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.IdProperty = Guid.Empty;
            });

            //Assert
            Assert.That(
                MessagesExceptions.GuidNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void DateSale_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.DateSale = DateTime.Now.AddYears(10);
            });

            //Assert
            Assert.That(
                MessagesExceptions.DateSaleCannotBeInFuture,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetName_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.Name = "Ho";
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

            //Act
            PropertyTrace propertyTrace = new()
            {
                Name = name
            };

            //Assert
            Assert.That(
                name,
                Is.EqualTo(propertyTrace.Name)
            );
        }

        [Test]
        public void SetValue_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.Value = 0;
            });

            //Assert
            Assert.That(
                MessagesExceptions.ValueMustBePositive,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetValue_ValueBaseInvalid_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.Value = 100;
            });

            //Assert
            Assert.That(
                MessagesExceptions.ValueTooLow,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetValue_Ok()
        {
            //Arrange
            int value = 1001;

            //Act 
            PropertyTrace propertyTrace = new()
            {
                Value = value
            };

            //Assert
            Assert.That(
                value,
                Is.EqualTo(propertyTrace.Value)
            );
        }

        [Test]
        public void SetTax_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.Tax = -1;
            });

            //Assert
            Assert.That(
                MessagesExceptions.TaxCannotBeNegative,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetTax_TaxBaseInvalid_Failed()
        {
            //Arrange
            PropertyTrace propertyTrace = new();

            //Act
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyTrace.Tax = 101;
            });

            //Assert
            Assert.That(
                MessagesExceptions.TaxCannotExceed100Percent,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetTax_Ok()
        {
            //Arrange
            int tax = 100;
            int value = 1001;

            //Act 
            PropertyTrace propertyTrace = new()
            {
                Value = value,
                Tax = tax
            };

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(
                    tax,
                    Is.EqualTo(propertyTrace.Tax)
                );
                Assert.That(
                    value * (tax / 100m),
                    Is.EqualTo(propertyTrace.TotalTax)
                );
            });
        }
    }
}
