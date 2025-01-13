using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Domain.Tests.DataBuilder
{
    public class PropertyBuilder
    {
        private Guid _idProperty;
        private Guid _idOwner;
        private string _name;
        private string _address;
        private int _price;
        private string _codeInternal;
        private int _year;
        private Owner _owner;
        private IEnumerable<PropertyImage> _propertyImages;
        private IEnumerable<PropertyTrace> _propertyTraces;

        public PropertyBuilder()
        {
            _idProperty = Guid.NewGuid();
            _idOwner = Guid.NewGuid();
            _name = "Default Property Name";
            _address = "123 Default St";
            _price = 100000;
            _codeInternal = "DEF1";
            _year = DateTime.Now.Year;
            _owner = new Owner
            {
                IdOwner = _idOwner,
                Name = "Default Owner",
                Address = "456 Owner St",
                Photo = "https://example.com/default-owner.jpg",
                Birthday = DateTime.Now.AddYears(-30)
            };
            _propertyImages = new List<PropertyImage>();
            _propertyTraces = new List<PropertyTrace>();
        }

        public Property Build()
        {
            return new Property
            {
                IdProperty = _idProperty,
                IdOwner = _idOwner,
                Name = _name,
                Address = _address,
                Price = _price,
                CodeInternal = _codeInternal,
                Year = _year,
                Owners = _owner,
                PropertyImages = _propertyImages,
                PropertyTraces = _propertyTraces
            };
        }

        public PropertyBuilder WithIdProperty(Guid idProperty)
        {
            _idProperty = idProperty;
            return this;
        }

        public PropertyBuilder WithIdOwner(Guid idOwner)
        {
            _idOwner = idOwner;
            return this;
        }

        public PropertyBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PropertyBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public PropertyBuilder WithPrice(int price)
        {
            _price = price;
            return this;
        }

        public PropertyBuilder WithCodeInternal(string codeInternal)
        {
            _codeInternal = codeInternal;
            return this;
        }

        public PropertyBuilder WithYear(int year)
        {
            _year = year;
            return this;
        }

        public PropertyBuilder WithOwner(Owner owner)
        {
            _owner = owner;
            return this;
        }

        public PropertyBuilder WithPropertyImages(IEnumerable<PropertyImage> propertyImages)
        {
            _propertyImages = propertyImages;
            return this;
        }

        public PropertyBuilder WithPropertyTraces(IEnumerable<PropertyTrace> propertyTraces)
        {
            _propertyTraces = propertyTraces;
            return this;
        }
    }
}
