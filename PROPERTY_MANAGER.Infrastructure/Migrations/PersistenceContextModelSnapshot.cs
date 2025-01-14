﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROPERTY_MANAGER.Infrastructure.Context;

#nullable disable

namespace PROPERTY_MANAGER.Infrastructure.Migrations
{
    [DbContext(typeof(PersistenceContext))]
    partial class PersistenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("IdOwner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOwner");

                    b.ToTable("Owner", "dbo");

                    b.HasData(
                        new
                        {
                            IdOwner = new Guid("c4cea593-40c2-4777-8012-217f37543d44"),
                            Address = "123 Main St, Los Angeles, CA",
                            Birthday = new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "John Smith",
                            Photo = "https://example.com/john_smith.png"
                        },
                        new
                        {
                            IdOwner = new Guid("7d31e56e-fdf2-4dcc-8a98-2024a4251b54"),
                            Address = "456 Elm St, Miami, FL",
                            Birthday = new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Emily Johnson",
                            Photo = "https://example.com/emily_johnson.png"
                        },
                        new
                        {
                            IdOwner = new Guid("1010000c-9f47-43b5-ae47-738cfa8a60aa"),
                            Address = "789 Oak St, Austin, TX",
                            Birthday = new DateTime(1975, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Michael Brown",
                            Photo = "https://example.com/michael_brown.png"
                        });
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("IdProperty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdOwner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("IdProperty");

                    b.HasIndex("IdOwner");

                    b.ToTable("Property", "dbo");

                    b.HasData(
                        new
                        {
                            IdProperty = new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0"),
                            Address = "101 Ocean Dr, Miami Beach, FL",
                            CodeInternal = "APT101",
                            IdOwner = new Guid("c4cea593-40c2-4777-8012-217f37543d44"),
                            Name = "Modern Apartment",
                            Price = 500000,
                            Year = 2020
                        },
                        new
                        {
                            IdProperty = new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8"),
                            Address = "202 Beverly Hills, Los Angeles, CA",
                            CodeInternal = "VILLA202",
                            IdOwner = new Guid("7d31e56e-fdf2-4dcc-8a98-2024a4251b54"),
                            Name = "Luxury Villa",
                            Price = 2500000,
                            Year = 2018
                        },
                        new
                        {
                            IdProperty = new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b"),
                            Address = "303 Lakeview Dr, Austin, TX",
                            CodeInternal = "COTTAGE303",
                            IdOwner = new Guid("1010000c-9f47-43b5-ae47-738cfa8a60aa"),
                            Name = "Cozy Cottage",
                            Price = 350000,
                            Year = 2015
                        });
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.PropertyImage", b =>
                {
                    b.Property<Guid>("IdPropertyImage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdProperty")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdPropertyImage");

                    b.HasIndex("IdProperty");

                    b.ToTable("PropertyImage", "dbo");

                    b.HasData(
                        new
                        {
                            IdPropertyImage = new Guid("7ae9d6b1-084e-4f38-8ae0-99a8ae858853"),
                            Enabled = true,
                            File = "path/file1.png",
                            IdProperty = new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0")
                        },
                        new
                        {
                            IdPropertyImage = new Guid("7aa867a3-e8a3-40e3-93ef-399aba11941e"),
                            Enabled = true,
                            File = "path/file2.png",
                            IdProperty = new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8")
                        },
                        new
                        {
                            IdPropertyImage = new Guid("096a407a-7e68-46ee-a7bc-f902ea78d22b"),
                            Enabled = true,
                            File = "path/file3.png",
                            IdProperty = new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b")
                        });
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.PropertyTrace", b =>
                {
                    b.Property<Guid>("IdPropertyTrace")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdProperty")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("IdPropertyTrace");

                    b.HasIndex("IdProperty");

                    b.ToTable("PropertyTrace", "dbo");

                    b.HasData(
                        new
                        {
                            IdPropertyTrace = new Guid("b2249477-3999-42fb-964d-5a95b24b7675"),
                            DateSale = new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProperty = new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0"),
                            Name = "Initial Sale",
                            Tax = 19,
                            Value = 500000
                        },
                        new
                        {
                            IdPropertyTrace = new Guid("f4acfe08-7e72-48ce-8a8c-a220be997377"),
                            DateSale = new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProperty = new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8"),
                            Name = "Renovation",
                            Tax = 19,
                            Value = 600000
                        },
                        new
                        {
                            IdPropertyTrace = new Guid("4ac93fee-8ceb-46b6-8089-a223053b4d6c"),
                            DateSale = new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProperty = new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b"),
                            Name = "Price Adjustment",
                            Tax = 19,
                            Value = 400000
                        });
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.Property", b =>
                {
                    b.HasOne("PROPERTY_MANAGER.Domain.Entities.Owner", "Owners")
                        .WithMany("Properties")
                        .HasForeignKey("IdOwner")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.PropertyImage", b =>
                {
                    b.HasOne("PROPERTY_MANAGER.Domain.Entities.Property", "Properties")
                        .WithMany("PropertyImages")
                        .HasForeignKey("IdProperty")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.PropertyTrace", b =>
                {
                    b.HasOne("PROPERTY_MANAGER.Domain.Entities.Property", "Properties")
                        .WithMany("PropertyTraces")
                        .HasForeignKey("IdProperty")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("PROPERTY_MANAGER.Domain.Entities.Property", b =>
                {
                    b.Navigation("PropertyImages");

                    b.Navigation("PropertyTraces");
                });
#pragma warning restore 612, 618
        }
    }
}
