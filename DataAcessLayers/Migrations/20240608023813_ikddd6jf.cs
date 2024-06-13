using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class ikddd6jf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ishospital",
                table: "NotPayedmoney");

            migrationBuilder.AddColumn<bool>(
                name: "ishospital",
                table: "NotPayedmoneyHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ishospital",
                table: "NotPayedmoneyHistory");

            migrationBuilder.AddColumn<bool>(
                name: "ishospital",
                table: "NotPayedmoney",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
