using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_SHOP.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "product");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "product",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "product");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "product",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
