using PROPERTY_MANAGER.Api.Tests.DataBuilder;
using PROPERTY_MANAGER.Infrastructure.Context;

namespace PROPERTY_MANAGER.Api.Tests.ConfigureDataSeed
{
    public static class PropertyTraceSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            PropertyTraceBuilder _builderRegisterForDeleted = new();
            context.Add(
                _builderRegisterForDeleted
                    .WithIdPropertyTrace(Guid.Parse("1472ae86-ac58-4042-b16e-f57858c82c5c"))
                    .WithIdProperty(Guid.Parse("659e4432-df93-4b01-bc78-3f3d7bf0bfd2"))
                    .Build()
            );
        }
    }
}
