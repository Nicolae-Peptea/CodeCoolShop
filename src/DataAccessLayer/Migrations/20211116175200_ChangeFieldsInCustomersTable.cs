using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ChangeFieldsInCustomersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddressCity",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddressCountry",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "BillingAddressCountryCode",
                table: "Customers",
                type: "bigint",
                maxLength: 25,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddressLine1",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "BillingAddressZip",
                table: "Customers",
                type: "bigint",
                maxLength: 25,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BillingName",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddressCity",
                table: "Customers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddressCountry",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ShippingAddressCountryCode",
                table: "Customers",
                type: "bigint",
                maxLength: 25,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddressLine1",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ShippingAddressZip",
                table: "Customers",
                type: "bigint",
                maxLength: 25,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ShippingName",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddressCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddressCountry",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddressCountryCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddressLine1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddressZip",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddressCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddressCountry",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddressCountryCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddressLine1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingAddressZip",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingName",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
