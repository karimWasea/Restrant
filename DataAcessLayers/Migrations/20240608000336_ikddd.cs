using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class ikddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.DropIndex(
                name: "IX_NotPayedmoney_UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.DropColumn(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoney");

            migrationBuilder.AddColumn<string>(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicaionuserId",
                table: "NotPayedmoney",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoneyHistory_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                column: "UserNotPayedmoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoney_ApplicaionuserId",
                table: "NotPayedmoney",
                column: "ApplicaionuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_ApplicaionuserId",
                table: "NotPayedmoney",
                column: "ApplicaionuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                column: "UserNotPayedmoneyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory");

            migrationBuilder.DropIndex(
                name: "IX_NotPayedmoneyHistory_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory");

            migrationBuilder.DropIndex(
                name: "IX_NotPayedmoney_ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.DropColumn(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory");

            migrationBuilder.DropColumn(
                name: "ApplicaionuserId",
                table: "NotPayedmoney");

            migrationBuilder.AddColumn<string>(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoney",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NotPayedmoney_UserNotPayedmoneyId",
                table: "NotPayedmoney",
                column: "UserNotPayedmoneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoney_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoney",
                column: "UserNotPayedmoneyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
