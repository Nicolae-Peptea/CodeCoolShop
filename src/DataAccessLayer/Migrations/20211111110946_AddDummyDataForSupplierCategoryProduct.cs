using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddDummyDataForSupplierCategoryProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display.", "Tablet" },
                    { 2, "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell or just phone.", "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Digital content and services", "Amazon" },
                    { 2, "Computers", "Lenovo" },
                    { 3, "Consumer electronics, computer software, and online services.", "Apple" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 1, 1, "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", "Amazon Fire", 49.9m, 1 },
                    { 3, 1, "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", "Amazon Fire HD 8", 89.0m, 1 },
                    { 2, 1, "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", "Lenovo IdeaPad Miix 700", 479.0m, 2 },
                    { 4, 2, "The iPhone 12 is a new iPhone model developed by Apple Inc. It is part of a device family that was announced during a special event on October 13, 2020 to succeed the iPhone 11 line.", "Apple iPhone 12 Pro Max", 1239.0m, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
