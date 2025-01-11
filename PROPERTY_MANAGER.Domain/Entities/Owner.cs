using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;

namespace PROPERTY_MANAGER.Domain.Entities
{
    public class Owner
    {
        private Guid _idOwner;
        private string _name = string.Empty;
        private string _address = string.Empty;
        private string _photo = string.Empty;
        private DateTime _birthday;

        public virtual IEnumerable<Property> Properties { get; set; } = default!;

        public Guid IdOwner
        {
            get => _idOwner;
            set
            {
                value.CheckValidGuid();
                _idOwner = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 3)
                {
                    throw new AppException(MessagesExceptions.SizeFieldNameNotValid);
                }
                _name = value;
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                if (value.Length > 6)
                {
                    throw new AppException(MessagesExceptions.SizeFieldAddressNotValid);
                }
                _address = value;
            }
        }
        public string Photo
        {
            get => _photo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AppException(MessagesExceptions.PhotoCannotBeEmpty);
                }

                if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult) ||
                    !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    throw new AppException(MessagesExceptions.PhotoUrlNotValid);
                }

                if (!value.EndsWith(".jpg") && !value.EndsWith(".png") && !value.EndsWith(".jpeg"))
                {
                    throw new AppException(MessagesExceptions.PhotoFileTypeNotValid);
                }

                _photo = value;
            }
        }
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new AppException(MessagesExceptions.BirthdayCannotBeInFuture);
                }

                var age = DateTime.Now.Year - value.Year;
                if (value.Date > DateTime.Now.AddYears(-age)) age--;

                if (age < 18)
                {
                    throw new AppException(MessagesExceptions.AgeMustBeAtLeast18);
                }

                if (age > 100)
                {
                    throw new AppException(MessagesExceptions.AgeCannotExceed100);
                }

                _birthday = value;
            }
        }
    }
}
