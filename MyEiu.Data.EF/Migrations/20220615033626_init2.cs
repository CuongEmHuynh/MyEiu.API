using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEiu.Data.EF.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationUser_User_UserId",
                table: "NotificationUser");

            migrationBuilder.DropIndex(
                name: "IX_NotificationUser_UserId",
                table: "NotificationUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NotificationUser");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "NotificationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "NotificationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NotificationUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "NotificationUser");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "NotificationUser");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NotificationUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "NotificationUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUser_UserId",
                table: "NotificationUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationUser_User_UserId",
                table: "NotificationUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
