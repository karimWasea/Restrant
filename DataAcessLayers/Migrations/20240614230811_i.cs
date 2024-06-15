using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalOroprationtyp",
                table: "FinancialUserCashHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalOroprationtyp",
                table: "FinancialUserCashHistories");
        }
    }
}
