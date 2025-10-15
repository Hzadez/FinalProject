using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMenegmentDL.Migrations
{
    /// <inheritdoc />
    public partial class InvitationStatusRebuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InivitationId",
                table: "Participations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InivitationId",
                table: "Participations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
