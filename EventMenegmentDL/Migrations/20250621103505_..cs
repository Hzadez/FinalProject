using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMenegmentDL.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations");

            migrationBuilder.RenameColumn(
                name: "IvitationId",
                table: "Participations",
                newName: "InivitationId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserInvitations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Participations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_UserId",
                table: "Participations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_AspNetUsers_UserId",
                table: "Participations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_AspNetUsers_UserId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInvitations_AspNetUsers_UserId",
                table: "UserInvitations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_UserId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Participations");

            migrationBuilder.RenameColumn(
                name: "InivitationId",
                table: "Participations",
                newName: "IvitationId");

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
    }
}
