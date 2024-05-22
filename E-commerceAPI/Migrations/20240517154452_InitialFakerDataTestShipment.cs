using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialFakerDataTestShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Code_Postal", "Country", "Date", "Region" },
                values: new object[] { "817 Alejandra Hollow", "Port Shanny", "65208", "Sudan", new DateTime(2024, 9, 5, 1, 35, 19, 85, DateTimeKind.Local).AddTicks(7342), "Massachusetts" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Code_Postal", "Country", "Date", "Region" },
                values: new object[] { "128 Eric Park", "Starkbury", "27588", "Tuvalu", new DateTime(2024, 10, 24, 1, 15, 15, 106, DateTimeKind.Local).AddTicks(2669), "Oklahoma" });
        }
    }
}
