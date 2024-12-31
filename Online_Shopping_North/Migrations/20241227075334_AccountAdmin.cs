using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping_North.Migrations
{
    /// <inheritdoc />
    public partial class AccountAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Percentage",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxDiscount",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BranchId", "Dob", "Email", "Gender", "Name", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("32aecf7c-3670-42ba-bd24-c4173b2452df"), null, new DateOnly(2004, 1, 20), "adminN@gmail.com", "Nam", "Trần Quốc Ấn", "20012004", "0939771198", "Admin", "adminN" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BranchId", "CustomerId", "DistrictId", "EmployeeId", "IsDefault", "Street" },
                values: new object[] { new Guid("fd781de2-93a7-421c-a0dc-8f7a6279c04e"), null, null, new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"), new Guid("32aecf7c-3670-42ba-bd24-c4173b2452df"), true, "2012004" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("fd781de2-93a7-421c-a0dc-8f7a6279c04e"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("32aecf7c-3670-42ba-bd24-c4173b2452df"));

            migrationBuilder.AlterColumn<int>(
                name: "Percentage",
                table: "Vouchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxDiscount",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
