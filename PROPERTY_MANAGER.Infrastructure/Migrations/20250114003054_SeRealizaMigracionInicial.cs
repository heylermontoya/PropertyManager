using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROPERTY_MANAGER.Infrastructure.Migrations
{
    public partial class SeRealizaMigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Owner",
                schema: "dbo",
                columns: table => new
                {
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "dbo",
                columns: table => new
                {
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CodeInternal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IdOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.IdProperty);
                    table.ForeignKey(
                        name: "FK_Property_Owner_IdOwner",
                        column: x => x.IdOwner,
                        principalSchema: "dbo",
                        principalTable: "Owner",
                        principalColumn: "IdOwner");
                });

            migrationBuilder.CreateTable(
                name: "PropertyImage",
                schema: "dbo",
                columns: table => new
                {
                    IdPropertyImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImage", x => x.IdPropertyImage);
                    table.ForeignKey(
                        name: "FK_PropertyImage_Property_IdProperty",
                        column: x => x.IdProperty,
                        principalSchema: "dbo",
                        principalTable: "Property",
                        principalColumn: "IdProperty");
                });

            migrationBuilder.CreateTable(
                name: "PropertyTrace",
                schema: "dbo",
                columns: table => new
                {
                    IdPropertyTrace = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false),
                    IdProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTrace", x => x.IdPropertyTrace);
                    table.ForeignKey(
                        name: "FK_PropertyTrace_Property_IdProperty",
                        column: x => x.IdProperty,
                        principalSchema: "dbo",
                        principalTable: "Property",
                        principalColumn: "IdProperty");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Owner",
                columns: new[] { "IdOwner", "Address", "Birthday", "Name", "Photo" },
                values: new object[] { new Guid("1010000c-9f47-43b5-ae47-738cfa8a60aa"), "789 Oak St, Austin, TX", new DateTime(1975, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Brown", "https://example.com/michael_brown.png" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Owner",
                columns: new[] { "IdOwner", "Address", "Birthday", "Name", "Photo" },
                values: new object[] { new Guid("7d31e56e-fdf2-4dcc-8a98-2024a4251b54"), "456 Elm St, Miami, FL", new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily Johnson", "https://example.com/emily_johnson.png" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Owner",
                columns: new[] { "IdOwner", "Address", "Birthday", "Name", "Photo" },
                values: new object[] { new Guid("c4cea593-40c2-4777-8012-217f37543d44"), "123 Main St, Los Angeles, CA", new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Smith", "https://example.com/john_smith.png" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Property",
                columns: new[] { "IdProperty", "Address", "CodeInternal", "IdOwner", "Name", "Price", "Year" },
                values: new object[] { new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8"), "202 Beverly Hills, Los Angeles, CA", "VILLA202", new Guid("7d31e56e-fdf2-4dcc-8a98-2024a4251b54"), "Luxury Villa", 2500000, 2018 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Property",
                columns: new[] { "IdProperty", "Address", "CodeInternal", "IdOwner", "Name", "Price", "Year" },
                values: new object[] { new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0"), "101 Ocean Dr, Miami Beach, FL", "APT101", new Guid("c4cea593-40c2-4777-8012-217f37543d44"), "Modern Apartment", 500000, 2020 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Property",
                columns: new[] { "IdProperty", "Address", "CodeInternal", "IdOwner", "Name", "Price", "Year" },
                values: new object[] { new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b"), "303 Lakeview Dr, Austin, TX", "COTTAGE303", new Guid("1010000c-9f47-43b5-ae47-738cfa8a60aa"), "Cozy Cottage", 350000, 2015 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PropertyImage",
                columns: new[] { "IdPropertyImage", "Enabled", "File", "IdProperty" },
                values: new object[,]
                {
                    { new Guid("096a407a-7e68-46ee-a7bc-f902ea78d22b"), true, "path/file3.png", new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b") },
                    { new Guid("7aa867a3-e8a3-40e3-93ef-399aba11941e"), true, "path/file2.png", new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8") },
                    { new Guid("7ae9d6b1-084e-4f38-8ae0-99a8ae858853"), true, "path/file1.png", new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0") }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "PropertyTrace",
                columns: new[] { "IdPropertyTrace", "DateSale", "IdProperty", "Name", "Tax", "Value" },
                values: new object[,]
                {
                    { new Guid("4ac93fee-8ceb-46b6-8089-a223053b4d6c"), new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("820c028d-b8c9-4641-bcf5-e59ecaf85c6b"), "Price Adjustment", 19, 400000 },
                    { new Guid("b2249477-3999-42fb-964d-5a95b24b7675"), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3b04b935-1879-40c9-921c-82f6acefcdd0"), "Initial Sale", 19, 500000 },
                    { new Guid("f4acfe08-7e72-48ce-8a8c-a220be997377"), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("350966d8-6d6b-4b0d-8049-4b71ea2f16c8"), "Renovation", 19, 600000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_IdOwner",
                schema: "dbo",
                table: "Property",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_IdProperty",
                schema: "dbo",
                table: "PropertyImage",
                column: "IdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTrace_IdProperty",
                schema: "dbo",
                table: "PropertyTrace",
                column: "IdProperty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyTrace",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Property",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Owner",
                schema: "dbo");
        }
    }
}
