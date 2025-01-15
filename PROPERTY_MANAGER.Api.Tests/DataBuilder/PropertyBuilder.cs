using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Api.Tests.DataBuilder
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

        public PropertyBuilder()
        {
            _idProperty = Guid.NewGuid();
            _idOwner = Guid.NewGuid();
            _name = "Default Property Name";
            _address = "123 Default St";
            _price = 100000;
            _codeInternal = "DEF1";
            _year = DateTime.Now.Year;
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
                Year = _year
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
    }
}
