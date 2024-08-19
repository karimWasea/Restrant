using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class PaymentRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TotalPaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalUnpaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnpaidMoneyId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_NotPayedmoney_UnpaidMoneyId",
                        column: x => x.UnpaidMoneyId,
                        principalTable: "NotPayedmoney",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_UnpaidMoneyId",
                table: "PaymentRecords",
                column: "UnpaidMoneyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRecords");
        }
    }
}
