using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class hdxeddffdddيd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "catid",
                table: "ShopingCaterCashHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "ShopingCaterCashHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "ShopingCaterCashHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catid",
                table: "ShopingCaterCashHistory");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "ShopingCaterCashHistory");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "ShopingCaterCashHistory");
        }
    }
}
