using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class AdminSouthAndCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BranchId", "Dob", "Email", "Gender", "Name", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"), null, new DateOnly(2004, 1, 1), "adminS@gmail.com", "Nam", "Đỗ Trần Tuấn Anh", "tuananh", "0256478983", "Admin", "adminS" },
                    { new Guid("94bf05f6-e5ff-45c6-8174-c4920edce47f"), null, new DateOnly(2004, 7, 7), "adminC@gmail.com", "Nam", "Lê Hồng Anh", "lehonganh", "0154789324", "Admin", "adminC" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BranchId", "CustomerId", "DistrictId", "EmployeeId", "IsDefault", "Street" },
                values: new object[,]
                {
                    { new Guid("a4e84922-0b61-4031-93ba-b074e85f7197"), null, null, new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"), new Guid("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"), true, "Miền Nam, HCM, Q1" },
                    { new Guid("e296a8c8-fdb9-4009-b6ff-ba8e1e2a7a3c"), null, null, new Guid("9fc84eab-708a-49a2-b819-06d0629e560a"), new Guid("94bf05f6-e5ff-45c6-8174-c4920edce47f"), true, "Miền Trung, Đà Nẵng, Hoàng Sa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("a4e84922-0b61-4031-93ba-b074e85f7197"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("e296a8c8-fdb9-4009-b6ff-ba8e1e2a7a3c"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("94bf05f6-e5ff-45c6-8174-c4920edce47f"));
        }
    }
}
