using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;

namespace PROPERTY_MANAGER.Domain.Entities
{
    public class PropertyTrace
    {
        private Guid _idProperty;
        private Guid _idPropertyTrace;
        private string _name = string.Empty;
        private int _tax;
        private int _value;
        private DateTime _dateSale;

        public virtual Property Properties { get; set; } = default!;

        public Guid IdPropertyTrace
        {
            get => _idPropertyTrace;
            set
            {
                value.CheckValidGuid();
                _idPropertyTrace = value;
            }
        }
        public DateTime DateSale
        {
            get => _dateSale;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new AppException(MessagesExceptions.DateSaleCannotBeInFuture);
                }

                _dateSale = value;
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
        public int Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                {
                    throw new AppException(MessagesExceptions.ValueMustBePositive);
                }

                if (value < 1000)
                {
                    throw new AppException(MessagesExceptions.ValueTooLow);
                }

                _value = value;
            }
        }
        public int Tax
        {
            get => _tax;
            set
            {
                if (value < 0)
                {
                    throw new AppException(MessagesExceptions.TaxCannotBeNegative);
                }

                if (value > 100)
                {
                    throw new AppException(MessagesExceptions.TaxCannotExceed100Percent);
                }

                _tax = value;
            }
        }
        public decimal TotalTax => Value * (Tax / 100m);

        public Guid IdProperty
        {
            get => _idProperty;
            set
            {
                value.CheckValidGuid();
                _idProperty = value;
            }
        }
    }
}
