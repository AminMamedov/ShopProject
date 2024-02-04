using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class CreatedARegistrAndURegistrColumnINUserT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignIn",
                table: "Users",
                newName: "URegistr");

            migrationBuilder.AddColumn<bool>(
                name: "ARegistr",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ARegistr",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "URegistr",
                table: "Users",
                newName: "SignIn");
        }
    }
}
