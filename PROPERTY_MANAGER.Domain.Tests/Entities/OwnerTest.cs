using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Exceptions;

namespace PROPERTY_MANAGER.Domain.Tests.Entities
{
    public class OwnerTest
    {
        [Test]
        public void SetIdOwner_Failed()
        {
            //Arrange
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.IdOwner = Guid.Empty;
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
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Name = "Ho";
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
            Owner owner = new();

            //Act & Assert
            owner.Name = name;

            //Assert
            Assert.That(
                name,
                Is.EqualTo(owner.Name)
            );
        }

        [Test]
        public void SetAddress_Failed()
        {
            //Arrange
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Address = "Ho";
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
            Owner owner = new();

            //Act & Assert            
            owner.Address = address;

            //Assert
            Assert.That(
                address,
                Is.EqualTo(owner.Address)
            );
        }

        [Test]
        public void SetPhoto_Failed()
        {
            //Arrange
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Photo = "";
            });

            //Assert
            Assert.That(
                MessagesExceptions.PhotoCannotBeEmpty,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetPhoto_WithExtensionSvg_Failed()
        {
            //Arrange
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Photo = "https://example.com/emily_johnson.svg";
            });

            //Assert
            Assert.That(
                MessagesExceptions.PhotoFileTypeNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetPhoto_WithUriInvalid_Failed()
        {
            //Arrange
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Photo = "johnson.jpg";
            });

            //Assert
            Assert.That(
                MessagesExceptions.PhotoUrlNotValid,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetBirthday_BirthdayCannotBeInFuture_Failed()
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddDays(20);
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Birthday = birthday;
            });

            //Assert
            Assert.That(
                MessagesExceptions.BirthdayCannotBeInFuture,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetBirthday_AgeMustBeAtLeast18_Failed()
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddYears(-17);
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Birthday = birthday;
            });

            //Assert
            Assert.That(
                MessagesExceptions.AgeMustBeAtLeast18,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetBirthday_Age18_Ok()
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddYears(-18);
            Owner owner = new();

            //Act & Assert            
            owner.Birthday = birthday;

            //Assert
            Assert.That(
                birthday,
                Is.EqualTo(owner.Birthday)
            );
        }

        [Test]
        public void SetBirthday_AgeCannotExceed100_Failed()
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddYears(-101);
            Owner owner = new();

            //Act & Assert
            AppException exception = Assert.Throws<AppException>(() =>
            {
                owner.Birthday = birthday;
            });

            //Assert
            Assert.That(
                MessagesExceptions.AgeCannotExceed100,
                Is.EqualTo(exception.Message)
            );
        }

        [Test]
        public void SetBirthday_Age100_Ok()
        {
            //Arrange
            DateTime birthday = DateTime.Now.AddYears(-100);
            Owner owner = new();

            //Act & Assert            
            owner.Birthday = birthday;

            //Assert
            Assert.That(
                birthday,
                Is.EqualTo(owner.Birthday)
            );
        }
    }
}
