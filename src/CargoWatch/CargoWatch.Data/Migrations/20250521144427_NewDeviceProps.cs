using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewDeviceProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Devices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Diapazon",
                table: "Devices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Diapazon",
                table: "Devices");
        }
    }
}
