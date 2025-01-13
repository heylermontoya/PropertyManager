using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;

namespace PROPERTY_MANAGER.Domain.Entities
{
    public class PropertyImage
    {
        private Guid _idProperty;
        private Guid _idPropertyImage;
        private string _file = default!;

        public virtual Property Properties { get; set; } = default!;

        public Guid IdPropertyImage
        {
            get => _idPropertyImage;
            set
            {
                value.CheckValidGuid();
                _idPropertyImage = value;
            }
        }
        public Guid IdProperty
        {
            get => _idProperty;
            set
            {
                value.CheckValidGuid();
                _idProperty = value;
            }
        }
        public string File
        {
            get => _file;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AppException(MessagesExceptions.FileCannotBeEmpty);
                }

                if (!value.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !value.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !value.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    throw new AppException(MessagesExceptions.FileTypeNotValid);
                }

                _file = value;
            }

        }
        public bool Enabled { get; set; }
    }
}
