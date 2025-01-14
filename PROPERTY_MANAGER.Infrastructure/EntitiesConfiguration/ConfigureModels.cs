using Microsoft.EntityFrameworkCore;
using PROPERTY_MANAGER.Domain.Entities;

namespace PROPERTY_MANAGER.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureModels
    {
        #region Constant

        //Ids Owners
        public readonly static Guid OwnerOneId = Guid.Parse("c4cea593-40c2-4777-8012-217f37543d44");
        public readonly static Guid OwnerTwoId = Guid.Parse("7d31e56e-fdf2-4dcc-8a98-2024a4251b54");
        public readonly static Guid OwnerThreeId = Guid.Parse("1010000c-9f47-43b5-ae47-738cfa8a60aa");

        //Ids Property
        public readonly static Guid PropertyOneId = Guid.Parse("3b04b935-1879-40c9-921c-82f6acefcdd0");
        public readonly static Guid PropertyTwoId = Guid.Parse("350966d8-6d6b-4b0d-8049-4b71ea2f16c8");
        public readonly static Guid PropertyThreeId = Guid.Parse("820c028d-b8c9-4641-bcf5-e59ecaf85c6b");

        #endregion

        internal static void ConfigureModel(this ModelBuilder modelBuilder)
        {
            #region Owner

            modelBuilder.Entity<Owner>()
                .HasKey(e => e.IdOwner);

            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    IdOwner = OwnerOneId,
                    Name = "John Smith",
                    Address = "123 Main St, Los Angeles, CA",
                    Photo = "https://example.com/john_smith.png",
                    Birthday = new DateTime(1980, 5, 15)
                },
                new Owner
                {
                    IdOwner = OwnerTwoId,
                    Name = "Emily Johnson",
                    Address = "456 Elm St, Miami, FL",
                    Photo = "https://example.com/emily_johnson.png",
                    Birthday = new DateTime(1990, 8, 20)
                },
                new Owner
                {
                    IdOwner = OwnerThreeId,
                    Name = "Michael Brown",
                    Address = "789 Oak St, Austin, TX",
                    Photo = "https://example.com/michael_brown.png",
                    Birthday = new DateTime(1975, 3, 10)
                }
            );

            #endregion

            #region Property

            modelBuilder.Entity<Property>()
                .HasKey(e => e.IdProperty);

            modelBuilder.Entity<Property>()
                .HasOne(property => property.Owners)
                .WithMany(owners => owners.Properties)
                .HasForeignKey(property => property.IdOwner)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    IdProperty = PropertyOneId,
                    Name = "Modern Apartment",
                    Address = "101 Ocean Dr, Miami Beach, FL",
                    Price = 500000,
                    CodeInternal = "APT101",
                    Year = 2020,
                    IdOwner = OwnerOneId
                },
                new Property
                {
                    IdProperty = PropertyTwoId,
                    Name = "Luxury Villa",
                    Address = "202 Beverly Hills, Los Angeles, CA",
                    Price = 2500000,
                    CodeInternal = "VILLA202",
                    Year = 2018,
                    IdOwner = OwnerTwoId
                },
                new Property
                {
                    IdProperty = PropertyThreeId,
                    Name = "Cozy Cottage",
                    Address = "303 Lakeview Dr, Austin, TX",
                    Price = 350000,
                    CodeInternal = "COTTAGE303",
                    Year = 2015,
                    IdOwner = OwnerThreeId
                }
            );

            #endregion

            #region PropertyImage

            modelBuilder.Entity<PropertyImage>()
                .HasKey(e => e.IdPropertyImage);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(propertyImage => propertyImage.Properties)
                .WithMany(properties => properties.PropertyImages)
                .HasForeignKey(propertyImage => propertyImage.IdProperty)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PropertyImage>().HasData(
                new PropertyImage()
                {
                    IdPropertyImage = Guid.NewGuid(),
                    IdProperty = PropertyOneId,
                    File = "path/file1.png",
                    Enabled = true
                },
                new PropertyImage()
                {
                    IdPropertyImage = Guid.NewGuid(),
                    IdProperty = PropertyTwoId,
                    File = "path/file2.png",
                    Enabled = true
                },
                new PropertyImage()
                {
                    IdPropertyImage = Guid.NewGuid(),
                    IdProperty = PropertyThreeId,
                    File = "path/file3.png",
                    Enabled = true
                }
            );

            #endregion

            #region PropertyTrace

            modelBuilder.Entity<PropertyTrace>()
                .HasKey(e => e.IdPropertyTrace);

            modelBuilder.Entity<PropertyTrace>()
                .HasOne(propertyTrace => propertyTrace.Properties)
                .WithMany(properties => properties.PropertyTraces)
                .HasForeignKey(propertyTrace => propertyTrace.IdProperty)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PropertyTrace>().HasData(
                new PropertyTrace()
                {
                    IdPropertyTrace = Guid.NewGuid(),
                    DateSale = new DateTime(2022, 1, 15),
                    Name = "Initial Sale",
                    Value = 500000,
                    Tax = 19,
                    IdProperty = PropertyOneId
                },
                new PropertyTrace()
                {
                    IdPropertyTrace = Guid.NewGuid(),
                    DateSale = new DateTime(2021, 5, 10),
                    Name = "Renovation",
                    Value = 600000,
                    Tax = 19,
                    IdProperty = PropertyTwoId
                },
                new PropertyTrace()
                {
                    IdPropertyTrace = Guid.NewGuid(),
                    DateSale = new DateTime(2023, 3, 20),
                    Name = "Price Adjustment",
                    Value = 400000,
                    Tax = 19,
                    IdProperty = PropertyThreeId
                }
            );
            #endregion
        }
    }
}
