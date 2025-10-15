using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMenegmentDL.Migrations
{
    /// <inheritdoc />
    public partial class _111111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInvitations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInvitations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
