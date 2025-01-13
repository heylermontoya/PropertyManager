using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Exceptions;

namespace PROPERTY_MANAGER.Domain.Tests.Entities
{
    public class PropertyImageTest
    {
        [Test]
        public void SetIdPropertyImage_Failed()
        {
            //Arrange
            PropertyImage propertyImage = new PropertyImage();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyImage.IdPropertyImage = Guid.Empty;
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
            PropertyImage propertyImage = new PropertyImage();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyImage.IdProperty = Guid.Empty;
            });

            //Assert
            Assert.That(
                MessagesExceptions.GuidNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetFile_Failed()
        {
            //Arrange
            PropertyImage propertyImage = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyImage.File = "";
            });

            //Assert
            Assert.That(
                MessagesExceptions.FileCannotBeEmpty,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetFile_WithExtensionSvg_Failed()
        {
            //Arrange
            PropertyImage propertyImage = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                propertyImage.File = "https://example.com/emily_johnson.svg";
            });

            //Assert
            Assert.That(
                MessagesExceptions.FileTypeNotValid,
                Is.EqualTo(exception.Message)
            );
        }
    }
}
