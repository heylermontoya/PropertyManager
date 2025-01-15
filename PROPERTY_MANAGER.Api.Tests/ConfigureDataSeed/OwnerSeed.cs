using PROPERTY_MANAGER.Api.Tests.DataBuilder;
using PROPERTY_MANAGER.Infrastructure.Context;

namespace PROPERTY_MANAGER.Api.Tests.ConfigureDataSeed
{
    public static class OwnerSeed
    {
        public static void ConfigureDataSeed(PersistenceContext context)
        {
            OwnerBuilder _builderRegisterForDeleted = new();
            context.Add(
                _builderRegisterForDeleted
                    .WithIdOwner(Guid.Parse("c87f84cf-8acf-4780-a75d-1f66cc3160b3"))
                    .Build()
            );
        }
    }
}
