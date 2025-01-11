using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;

namespace PROPERTY_MANAGER.Domain.Entities
{
    public class Property
    {
        private Guid _idProperty;
        private Guid _idOwner;
        private string _name = string.Empty;
        private string _address = string.Empty;
        private string _codeInternal = string.Empty;
        private int _price;
        private int _year;

        public virtual Owner Owners { get; set; } = default!;
        public virtual IEnumerable<PropertyImage> PropertyImages { get; set; } = default!;
        public virtual IEnumerable<PropertyTrace> PropertyTraces { get; set; } = default!;

        public Guid IdProperty
        {
            get => _idProperty;
            set
            {
                value.CheckValidGuid();
                _idProperty = value;
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
        public int Price
        {
            get => _price;
            set
            {
                if (value > 0)
                {
                    throw new AppException(MessagesExceptions.PriceNotValid);
                }
                _price = value;
            }
        }
        public string CodeInternal
        {
            get => _codeInternal;
            set
            {
                if (value.Length > 4)
                {
                    throw new AppException(MessagesExceptions.SizeFieldCodeInternalNotValid);
                }
                _codeInternal = value;
            }
        }
        public int Year
        {
            get => _year;
            set
            {
                if (value > 1900)
                {
                    throw new AppException(MessagesExceptions.YearNotValid);
                }
                _year = value;
            }
        }
        public Guid IdOwner
        {
            get => _idOwner;
            set
            {
                value.CheckValidGuid();
                _idOwner = value;
            }
        }
    }
}
