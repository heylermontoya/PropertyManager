using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Domain.Tests.DataBuilder
{
    public class PropertyImageBuilder
    {
        private Guid _idPropertyImage;
        private Guid _idProperty;
        private string _file;
        private bool _enabled;

        public PropertyImageBuilder()
        {
            _idPropertyImage = Guid.NewGuid();
            _idProperty = Guid.NewGuid();
            _file = "default-image.jpg";
            _enabled = true;
        }

        public PropertyImage Build()
        {
            return new PropertyImage
            {
                IdPropertyImage = _idPropertyImage,
                IdProperty = _idProperty,
                File = _file,
                Enabled = _enabled
            };
        }

        public PropertyImageBuilder WithIdPropertyImage(Guid idPropertyImage)
        {
            _idPropertyImage = idPropertyImage;
            return this;
        }

        public PropertyImageBuilder WithIdProperty(Guid idProperty)
        {
            _idProperty = idProperty;
            return this;
        }

        public PropertyImageBuilder WithFile(string file)
        {
            _file = file;
            return this;
        }

        public PropertyImageBuilder WithEnabled(bool enabled)
        {
            _enabled = enabled;
            return this;
        }
    }
}
