using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialFakerDataTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shipment",
                columns: new[] { "Id", "Address", "City", "Code_Postal", "Country", "Date", "IsDeleted", "Region" },
                values: new object[] { 1, "128 Eric Park", "Starkbury", "27588", "Tuvalu", new DateTime(2024, 10, 24, 1, 15, 15, 106, DateTimeKind.Local).AddTicks(2669), false, "Oklahoma" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
