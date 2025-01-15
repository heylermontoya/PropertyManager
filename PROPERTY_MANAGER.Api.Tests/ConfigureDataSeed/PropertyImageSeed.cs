using PROPERTY_MANAGER.Api.Tests.DataBuilder;
using PROPERTY_MANAGER.Infrastructure.Context;

namespace PROPERTY_MANAGER.Api.Tests.ConfigureDataSeed
{
    public static class PropertyImageSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            PropertyImageBuilder _builderRegisterForDeleted = new();
            context.Add(
                _builderRegisterForDeleted
                    .WithIdPropertyImage(Guid.Parse("aae41333-5511-4231-9e44-2cbc33c95153"))
                    .WithIdProperty(Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"))
                    .Build()
            );
        }
    }
}
