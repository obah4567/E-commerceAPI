using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialFakerDataTestProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Movies" },
                    { 2, "Tools" },
                    { 3, "Baby" },
                    { 4, "Kids" },
                    { 5, "Electronics" },
                    { 6, "Toys" },
                    { 7, "Industrial" },
                    { 8, "Grocery" },
                    { 9, "Automotive" },
                    { 10, "Books" }
                });

            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Code_Postal", "Country", "Date", "Region" },
                values: new object[] { "1321 Jess Landing", "Collinsport", "69950", "Gibraltar", new DateTime(2024, 6, 11, 14, 2, 54, 372, DateTimeKind.Local).AddTicks(6918), "Missouri" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Code_Postal", "Country", "Date", "Region" },
                values: new object[] { "817 Alejandra Hollow", "Port Shanny", "65208", "Sudan", new DateTime(2024, 9, 5, 1, 35, 19, 85, DateTimeKind.Local).AddTicks(7342), "Massachusetts" });
        }
    }
}
