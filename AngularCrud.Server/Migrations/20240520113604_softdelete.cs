using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularCrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class softdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Isdelete",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "Employee");
        }
    }
}
