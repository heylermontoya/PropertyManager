namespace PROPERTY_MANAGER.Application.DTOs
{
    public class PropertyDto
    {
        public Guid IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public Guid IdOwner { get; set; }
    }
}
