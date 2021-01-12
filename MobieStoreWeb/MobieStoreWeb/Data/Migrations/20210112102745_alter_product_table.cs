using Microsoft.EntityFrameworkCore.Migrations;

namespace MobieStoreWeb.Data.Migrations
{
    public partial class alter_product_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)5);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: "Sneaker");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: "Accessory");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: "Adidas");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: "Nike");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "Name",
                value: "Vans");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)4,
                column: "Name",
                value: "Convert");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)5,
                column: "Name",
                value: "Local brand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: "Mobile");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: "Tablet");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)3, "Watch" },
                    { (short)4, "Laptop" },
                    { (short)5, "Accessory" }
                });

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Name",
                value: "Apple");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Name",
                value: "Samsung");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "Name",
                value: "Oppo");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)4,
                column: "Name",
                value: "Xiaomi");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: (short)5,
                column: "Name",
                value: "Huawei");
        }
    }
}
