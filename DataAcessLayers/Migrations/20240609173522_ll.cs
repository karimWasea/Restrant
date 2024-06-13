using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class ll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalaoOrprationtyp",
                table: "ShopingCaterNotpayedHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HospitalaoOrprationtyp",
                table: "NotPayedmoneyHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalaoOrprationtyp",
                table: "ShopingCaterNotpayedHistory");

            migrationBuilder.DropColumn(
                name: "HospitalaoOrprationtyp",
                table: "NotPayedmoneyHistory");
        }
    }
}
