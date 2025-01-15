using PROPERTY_MANAGER.Api.Tests.DataBuilder;
using PROPERTY_MANAGER.Infrastructure.Context;

namespace PROPERTY_MANAGER.Api.Tests.ConfigureDataSeed
{
    public static class PropertySeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            PropertyBuilder _builderRegisterForDeleted = new();
            context.Add(
                _builderRegisterForDeleted
                    .WithIdProperty(Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"))
                    .WithIdOwner(Guid.Parse("c87f84cf-8acf-4780-a75d-1f66cc3160b3"))
                    .Build()
            );
        }
    }
}
