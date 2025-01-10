namespace PROPERTY_MANAGER.Domain.Entities
{
    internal class PropertyImage
    {
        public Guid IdPropertyImage {  get; set; }
        public Guid IdProperty { get; set; }
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
