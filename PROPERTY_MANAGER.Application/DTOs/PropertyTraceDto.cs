namespace PROPERTY_MANAGER.Application.DTOs
{
    public class PropertyTraceDto
    {
        public Guid IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Tax { get; set; }
        public Guid IdProperty { get; set; }
    }
}
