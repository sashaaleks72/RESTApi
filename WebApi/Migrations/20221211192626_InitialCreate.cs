using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Net7.WebApi.Test.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teapots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false),
                    WarrantyInMonths = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teapots", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Teapots",
                columns: new[] { "Id", "Capacity", "Description", "ImgUrl", "ManufacturerCountry", "Price", "Quantity", "Title", "WarrantyInMonths" },
                values: new object[,]
                {
                    { 1, 1.7, "Some good teapot", "image1.png", "Germany", 490.0, 6, "Teapot 1", 12 },
                    { 2, 1.8, "Some good teapot", "image2.png", "China", 515.0, 10, "Teapot 2", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teapots");
        }
    }
}
