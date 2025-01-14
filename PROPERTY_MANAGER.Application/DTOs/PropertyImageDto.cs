namespace PROPERTY_MANAGER.Application.DTOs
{
    public class PropertyImageDto
    {
        public Guid IdPropertyImage { get; set; }
        public Guid IdProperty { get; set; }
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
