using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddMoreDefaultProductsInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 8, 2, "128GB, 5G, Graphite", "Apple iPhone 13 Pro", 1159.9m, 3 },
                    { 12, 1, "10.2 inches, 32GB, Wi-Fi, Space Grey", "Apple iPad 8 (2020)", 1239.0m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 4, "Consumer electronics, computer software, and online services.", "Xiaomi" },
                    { 5, "Consumer electronics, computer software, and online services.", "Samsung" },
                    { 6, "Consumer electronics.", "Motorola" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 5, 2, "Dual SIM, 128GB, 6GB RAM, 4G, Tropical Green", "Xiaomi Redmi Note 9 Pro", 199.9m, 4 },
                    { 9, 1, "Octa-Core, 11 inches, 6GB RAM, 128GB, Wi-Fi, Cosmic Gray", "Xiaomi Pad 5", 369.9m, 4 },
                    { 7, 2, "Dual SIM, 128GB, 8GB RAM, 5G, Phantom Grey", "Samsung Galaxy S21", 809.0m, 5 },
                    { 11, 1, "Octa-Core, 10.4 inches, 3GB RAM, 32GB, 4G, Gray", "Samsung Galaxy Tab A7", 302.9m, 5 },
                    { 6, 1, "Octa-Core, 11 inches 2K IPS, 6GB RAM, 128GB, WiFi, Slate Grey", "Lenovo Tab P11 Plus", 279.0m, 6 },
                    { 10, 2, "Dual SIM, 128GB, 6GB RAM, Blue", "Motorola G60s", 199.8m, 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BillingAddressCity", "BillingAddressCountry", "BillingAddressCountryCode", "BillingAddressLine1", "BillingAddressZip", "BillingName", "Email", "FirstName", "LastName", "Phone", "ShippingAddressCity", "ShippingAddressCountry", "ShippingAddressCountryCode", "ShippingAddressLine1", "ShippingAddressZip", "ShippingName", "UserId" },
                values: new object[] { 1, "Topolog", null, 0L, null, null, null, null, "Ion", null, null, null, null, null, null, null, null, null });
        }
    }
}
