using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApp1.Migrations
{
    /// <inheritdoc />
    public partial class FeedDataTodatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "About", "AddressId", "Age", "Company", "Email", "EyeColor", "Gender", "Index", "Latitude", "Longitude", "Name", "Phone", "Registered", "Tags" },
                values: new object[] { 68, null, null, 0, null, null, null, null, 68, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "About", "AddressId", "Age", "Company", "Email", "EyeColor", "Gender", "Index", "Latitude", "Longitude", "Name", "Phone", "Registered", "Tags" },
                values: new object[] { 65, null, null, 0, null, null, null, null, 65, null, null, null, null, null, null });
        }
    }
}
