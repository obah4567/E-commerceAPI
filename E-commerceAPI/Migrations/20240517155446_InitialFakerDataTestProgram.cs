using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialFakerDataTestProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shipment",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Category_Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, null, "Description1", "Product1", 10.0 },
                    { 2, null, null, "Description2", "Product2", 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Shipment",
                columns: new[] { "Id", "Address", "City", "Code_Postal", "Country", "Date", "IsDeleted", "Region" },
                values: new object[] { 1, "1321 Jess Landing", "Collinsport", "69950", "Gibraltar", new DateTime(2024, 6, 11, 14, 2, 54, 372, DateTimeKind.Local).AddTicks(6918), false, "Missouri" });
        }
    }
}
