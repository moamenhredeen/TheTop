using Microsoft.EntityFrameworkCore.Migrations;

namespace TheTop.Application.Migrations
{
    public partial class something1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayValue",
                table: "CompanyInformations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "accountant-role-id",
                column: "ConcurrencyStamp",
                value: "db065224-dd12-4f00-b9fc-bf3cf4d485a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role-id",
                column: "ConcurrencyStamp",
                value: "9083cc12-9b42-4570-a8c2-a701c450587a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "customer-role-id",
                column: "ConcurrencyStamp",
                value: "0068b780-41eb-49de-a0f5-694d2c36a518");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Programmer-role-id",
                column: "ConcurrencyStamp",
                value: "21fcb1ff-5aca-470a-b0de-513993fbf92a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "923ef3bb-56c1-40d6-83ed-6a88db2d1079", "AQAAAAEAACcQAAAAEMjbML3lnSRLKJKXcQPbONx0LGUqZGnyMypm9SGccQRUcFOfZ9kFUHo4SzCLSo7PUQ==", "8c2f3b38-ae18-41fa-bf56-376293e152f5" });

            migrationBuilder.InsertData(
                table: "CompanyInformations",
                columns: new[] { "CompanyInformationId", "Key", "Value" },
                values: new object[] { 1, "xxx", "kenan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CompanyInformations",
                keyColumn: "CompanyInformationId",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "DisplayValue",
                table: "CompanyInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "accountant-role-id",
                column: "ConcurrencyStamp",
                value: "223b1185-d9d7-44db-9038-8b9ed219d36c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role-id",
                column: "ConcurrencyStamp",
                value: "ae1ae443-7d2d-4148-8855-88e5a72c745c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "customer-role-id",
                column: "ConcurrencyStamp",
                value: "63facb06-4f37-4d7e-8095-4350be390350");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Programmer-role-id",
                column: "ConcurrencyStamp",
                value: "095f6d8b-7a7d-496b-b35c-635131a1294b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0b7dfb9-6da1-49bd-b4f5-d45a17b55ce3", "AQAAAAEAACcQAAAAEK9QdV7RymZkv02cGHsh2lZk8AQKFGUNA7vD/nZrAKoMQoyf1NY5trJ1Jj5nwkKN9g==", "272dbb7d-a110-4e1c-8592-f9e843776350" });
        }
    }
}
