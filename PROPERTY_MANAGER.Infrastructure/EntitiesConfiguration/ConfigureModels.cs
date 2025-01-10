using Microsoft.EntityFrameworkCore;
using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureModels
    {
        internal static void ConfigureModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasKey(e => e.IdProperty);

            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    IdProperty = Guid.NewGuid(),
                    Name = "Property 1",
                    Address = "Address Property 1",
                    Price = 1000,
                    CodeInternal = "Code Internal Property 1",
                    Year = 2025,
                    IdOwner = Guid.NewGuid()
                },
                new Property
                {
                    IdProperty = Guid.NewGuid(),
                    Name = "Property 2",
                    Address = "Address Property 2",
                    Price = 1000,
                    CodeInternal = "Code Internal Property 2",
                    Year = 2025,
                    IdOwner = Guid.NewGuid()
                },
                new Property
                {
                    IdProperty = Guid.NewGuid(),
                    Name = "Property 3",
                    Address = "Address Property 3",
                    Price = 1000,
                    CodeInternal = "Code Internal Property 3",
                    Year = 2025,
                    IdOwner = Guid.NewGuid()
                }
            );
        }
    }
}
