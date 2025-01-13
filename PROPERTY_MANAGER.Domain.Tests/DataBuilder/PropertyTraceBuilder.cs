using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Domain.Tests.DataBuilder
{
    public class PropertyTraceBuilder
    {
        private Guid _idPropertyTrace;
        private Guid _idProperty;
        private string _name;
        private int _value;
        private int _tax;
        private DateTime _dateSale;
        private Property _property;

        public PropertyTraceBuilder()
        {
            _idPropertyTrace = Guid.NewGuid();
            _idProperty = Guid.NewGuid();
            _name = "Default Trace";
            _value = 1000;
            _tax = 10;
            _dateSale = DateTime.Now.AddDays(-1);
            _property = new Property
            {
                IdProperty = _idProperty,
                Name = "Default Property",
                Address = "123 Default St",
                Price = 100000,
                CodeInternal = "DEF123",
                Year = DateTime.Now.Year
            };
        }

        public PropertyTrace Build()
        {
            return new PropertyTrace
            {
                IdPropertyTrace = _idPropertyTrace,
                IdProperty = _idProperty,
                Name = _name,
                Value = _value,
                Tax = _tax,
                DateSale = _dateSale,
                Properties = _property
            };
        }

        public PropertyTraceBuilder WithIdPropertyTrace(Guid idPropertyTrace)
        {
            _idPropertyTrace = idPropertyTrace;
            return this;
        }

        public PropertyTraceBuilder WithIdProperty(Guid idProperty)
        {
            _idProperty = idProperty;
            return this;
        }

        public PropertyTraceBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PropertyTraceBuilder WithValue(int value)
        {
            _value = value;
            return this;
        }

        public PropertyTraceBuilder WithTax(int tax)
        {
            _tax = tax;
            return this;
        }

        public PropertyTraceBuilder WithDateSale(DateTime dateSale)
        {
            _dateSale = dateSale;
            return this;
        }

        public PropertyTraceBuilder WithProperty(Property property)
        {
            _property = property;
            return this;
        }
    }
}
