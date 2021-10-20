using Microsoft.EntityFrameworkCore.Migrations;

namespace TheTop.Application.Migrations
{
    public partial class something4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role-id",
                column: "ConcurrencyStamp",
                value: "ae1ae443-7d2d-4148-8855-88e5a72c745c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "customer-role-id", "63facb06-4f37-4d7e-8095-4350be390350", "Customer", "CUSTOMER" },
                    { "Programmer-role-id", "095f6d8b-7a7d-496b-b35c-635131a1294b", "Programmer", "PROGRAMMER" },
                    { "accountant-role-id", "223b1185-d9d7-44db-9038-8b9ed219d36c", "Accountant", "ACCOUNTANT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0b7dfb9-6da1-49bd-b4f5-d45a17b55ce3", "AQAAAAEAACcQAAAAEK9QdV7RymZkv02cGHsh2lZk8AQKFGUNA7vD/nZrAKoMQoyf1NY5trJ1Jj5nwkKN9g==", "272dbb7d-a110-4e1c-8592-f9e843776350" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "accountant-role-id");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "customer-role-id");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Programmer-role-id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role-id",
                column: "ConcurrencyStamp",
                value: "47f545b2-8957-4efd-896d-9028d8e5d35e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10c6ffb3-a8fa-4368-989e-5adbb4bfeb90", "AQAAAAEAACcQAAAAEDfC4CbY6Ba0PSfK0IMoyf/olusXqCszSm0L91Nc3/CixyEmrgI4iNPy8x9EQXlg0g==", "a644e305-410d-4956-9521-8e6a2a572f86" });
        }
    }
}
