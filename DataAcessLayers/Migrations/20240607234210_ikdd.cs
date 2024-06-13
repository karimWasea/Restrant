using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class ikdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.DropTable(
                name: "FinancialAdvanceHistories");

            migrationBuilder.DropIndex(
                name: "IX_NotPayedmoney_ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.DropColumn(
                name: "NotpayedUserid",
                table: "ShopingCaterCashHistory");

            migrationBuilder.DropColumn(
                name: "ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.RenameColumn(
                name: "PayedAmount",
                table: "ShopingCaterCashHistory",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<string>(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoney",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NotPayedmoneyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NotpayedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    NotPayedmoneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotPayedmoneyHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotPayedmoneyHistory_NotPayedmoney_NotPayedmoneyId",
                        column: x => x.NotPayedmoneyId,
                        principalTable: "NotPayedmoney",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopingCaterNotpayedHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qantity = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceProductebytypesId = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    catid = table.Column<int>(type: "int", nullable: false),
                    NotpayedUserid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopingCaterNotpayedHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotPayedmoneyHistoryPriceProductebytypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotPayedmoneyHistoryid = table.Column<int>(type: "int", nullable: false),
                    PriceProductebytypesid = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotPayedmoneyHistoryPriceProductebytypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotPayedmoneyHistoryPriceProductebytypes_NotPayedmoneyHistory_NotPayedmoneyHistoryid",
                        column: x => x.NotPayedmoneyHistoryid,
                        principalTable: "NotPayedmoneyHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotPayedmoneyHistoryPriceProductebytypes_PriceProductebytypes_PriceProductebytypesid",
                        column: x => x.PriceProductebytypesid,
                        principalTable: "PriceProductebytypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoney_UserNotPayedmoneyId",
                table: "NotPayedmoney",
                column: "UserNotPayedmoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoneyHistory_NotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                column: "NotPayedmoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoneyHistoryPriceProductebytypes_NotPayedmoneyHistoryid",
                table: "NotPayedmoneyHistoryPriceProductebytypes",
                column: "NotPayedmoneyHistoryid");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoneyHistoryPriceProductebytypes_PriceProductebytypesid",
                table: "NotPayedmoneyHistoryPriceProductebytypes",
                column: "PriceProductebytypesid");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoney",
                column: "UserNotPayedmoneyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.DropTable(
                name: "NotPayedmoneyHistoryPriceProductebytypes");

            migrationBuilder.DropTable(
                name: "ShopingCaterNotpayedHistory");

            migrationBuilder.DropTable(
                name: "NotPayedmoneyHistory");

            migrationBuilder.DropIndex(
                name: "IX_NotPayedmoney_UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.DropColumn(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "ShopingCaterCashHistory",
                newName: "PayedAmount");

            migrationBuilder.AddColumn<string>(
                name: "NotpayedUserid",
                table: "ShopingCaterCashHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicaionuserId",
                table: "NotPayedmoney",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FinancialAdvanceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotPayedmoneyId = table.Column<int>(type: "int", nullable: false),
                    PriceProductebytypesid = table.Column<int>(type: "int", nullable: false),
                    UserNotPayedmoneyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotpayedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PayedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    SystemUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemUserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAdvanceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialAdvanceHistories_AspNetUsers_UserNotPayedmoneyId",
                        column: x => x.UserNotPayedmoneyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinancialAdvanceHistories_NotPayedmoney_NotPayedmoneyId",
                        column: x => x.NotPayedmoneyId,
                        principalTable: "NotPayedmoney",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialAdvanceHistories_PriceProductebytypes_PriceProductebytypesid",
                        column: x => x.PriceProductebytypesid,
                        principalTable: "PriceProductebytypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoney_ApplicaionuserId",
                table: "NotPayedmoney",
                column: "ApplicaionuserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAdvanceHistories_NotPayedmoneyId",
                table: "FinancialAdvanceHistories",
                column: "NotPayedmoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAdvanceHistories_PriceProductebytypesid",
                table: "FinancialAdvanceHistories",
                column: "PriceProductebytypesid");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAdvanceHistories_UserNotPayedmoneyId",
                table: "FinancialAdvanceHistories",
                column: "UserNotPayedmoneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_ApplicaionuserId",
                table: "NotPayedmoney",
                column: "ApplicaionuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
