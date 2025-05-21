using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeededTemperature",
                table: "Devices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SSID",
                table: "Devices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SSID_Password",
                table: "Devices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeededTemperature",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SSID",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SSID_Password",
                table: "Devices");
        }
    }
}
