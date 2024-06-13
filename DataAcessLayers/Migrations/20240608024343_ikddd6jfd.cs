using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayers.Migrations
{
    /// <inheritdoc />
    public partial class ikddd6jfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory");

            migrationBuilder.AlterColumn<string>(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                column: "UserNotPayedmoneyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory");

            migrationBuilder.AlterColumn<string>(
                name: "UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotPayedmoneyHistory_AspNetUsers_UserNotPayedmoneyId",
                table: "NotPayedmoneyHistory",
                column: "UserNotPayedmoneyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
