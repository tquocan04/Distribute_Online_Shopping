using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping_South.Migrations
{
    /// <inheritdoc />
    public partial class AddressData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    MaxDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MinOrderValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCart = table.Column<bool>(type: "bit", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingMethodId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_ShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchProducts",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchProducts", x => new { x.ProductId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_BranchProducts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { "CASH", null, "CASH" },
                    { "MOMO", null, "MOMO" },
                    { "ZALOPAY", null, "ZALOPAY" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Bac", "Miền Bắc" },
                    { "Nam", "Miền Nam" },
                    { "Trung", "Miền Trung" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "Admin", "ADMIN" },
                    { "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "ShippingMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "BE", "BE" },
                    { "GRAB", "GRAB" },
                    { "SHOPEEFOOD", "SHOPEE FOOD" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Hải Phòng", "Bac" },
                    { new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Quảng Ninh", "Bac" },
                    { new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Hà Nội", "Bac" },
                    { new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Phú Thọ", "Bac" },
                    { new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Thừa-Thiên Huế", "Trung" },
                    { new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Hải Dương", "Bac" },
                    { new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Hồ Chí Minh", "Nam" },
                    { new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Quảng Trị", "Trung" },
                    { new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Đồng Nai", "Nam" },
                    { new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Long An", "Nam" },
                    { new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Quảng Nam", "Trung" },
                    { new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Bình Dương", "Nam" },
                    { new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Bà Rịa Vũng Tàu", "Nam" },
                    { new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Đà Nẵng", "Trung" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BranchId", "Dob", "Email", "Gender", "Name", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"), null, new DateOnly(2004, 1, 1), "adminS@gmail.com", "Nam", "Đỗ Trần Tuấn Anh", "tuananh", "0256478983", "Admin", "adminS" });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("033fbf94-3a48-4bec-a1d1-13f0dcca7ff2"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Lê Chân" },
                    { new Guid("05e87ab6-1238-412d-9d93-88902310ee89"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Trảng Bom" },
                    { new Guid("1bc7754e-bacc-484f-b5cc-7e2df41f1f30"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Hạ Long" },
                    { new Guid("1be59787-ee81-4824-b7e1-766e71fffa6b"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Uông Bí" },
                    { new Guid("1f461ef4-7dbc-4f7c-b51e-a16a5cfca7d3"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Gia Lộc" },
                    { new Guid("24de8ac0-247f-40e4-b472-1171b56c1e74"), new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Hội An" },
                    { new Guid("283942d0-07a6-44c4-a8e3-af3372c4f4d7"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Thị xã Bà Rịa" },
                    { new Guid("289f3f8b-7b77-491e-9892-043cee73f0a3"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Cẩm Phả" },
                    { new Guid("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"), new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Hải Châu" },
                    { new Guid("2f657b93-1d8b-4024-bfd9-827009d98c67"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Hải Dương" },
                    { new Guid("412da1ea-5f1b-4aa3-9c13-cf5d557b59e3"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Móng Cái" },
                    { new Guid("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Chí Linh" },
                    { new Guid("4b1dd408-19c8-40cc-a39a-02ecaf53cfbe"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Phong Điền" },
                    { new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 1" },
                    { new Guid("57fa0862-5fc6-4e0b-bc55-b8dda3860fc8"), new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Đức Huệ" },
                    { new Guid("5e9c531c-cf58-4f0b-a447-d1a1cfd2b1b6"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Phú Vang" },
                    { new Guid("603cf681-062c-4378-89d0-a534ab196661"), new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Cam Lộ" },
                    { new Guid("83ba4e24-9a0f-4c6a-b822-29a1eb5f4d3f"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Hoàn Kiếm" },
                    { new Guid("8c132e18-9710-402f-a5c0-7ad8dd90311b"), new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Tam Kỳ" },
                    { new Guid("8f23aec1-ad93-4170-8493-b388da9ec33e"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Tân Uyên" },
                    { new Guid("90c585ba-5d16-4406-ad97-41c34732ccd3"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Long Thành" },
                    { new Guid("91d1c4dd-f364-412d-a7a2-8857f4b8a9c9"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Thanh Thủy" },
                    { new Guid("93811fdc-4e5e-4e92-87dd-91650cfe357a"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Biên Hòa" },
                    { new Guid("9744402f-34c1-4555-baff-9450ab73303a"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Hồng Bàng" },
                    { new Guid("9d442ba3-12d2-4845-9a9c-92d88979bd96"), new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Tân An" },
                    { new Guid("9fc84eab-708a-49a2-b819-06d0629e560a"), new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Hoàng Sa" },
                    { new Guid("a9e0ff46-2f5c-463a-a793-08e9a533900c"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Hương Thủy" },
                    { new Guid("aab01227-c628-4ad0-a7c3-69ce33712109"), new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Hải Lăng" },
                    { new Guid("aabecff8-16d3-4298-839b-c5ec84ae49a3"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Kiến An" },
                    { new Guid("ab041f51-dcf0-4071-b1ae-6ebeaf3c4840"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Châu Đốc" },
                    { new Guid("af5c6bad-cba1-459c-aacc-e31438f4ba31"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Thủ Dầu Một" },
                    { new Guid("b051a4a6-66ba-4220-95e4-59abd37d4e0b"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Đống Đa" },
                    { new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Cầu Giấy" },
                    { new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Ba Đình" },
                    { new Guid("c56546a3-e495-4662-a7f4-9196eccbdbf7"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Dĩ An" },
                    { new Guid("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 5" },
                    { new Guid("db4e7867-1156-44d9-825b-8f42ae9712fe"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Yên Lập" },
                    { new Guid("e60650de-7772-49d5-ac72-81d3bfa774d4"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Long Khánh" },
                    { new Guid("eef65a95-e294-46a3-828f-5e44ca4b2c77"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Hạ Hòa" },
                    { new Guid("f6220d40-db0b-4be5-ada4-5996bab22cd0"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Xuyên Mộc" },
                    { new Guid("f9be5b5e-847d-47e9-847a-0150c4f608e1"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 10" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BranchId", "CustomerId", "DistrictId", "EmployeeId", "IsDefault", "Street" },
                values: new object[] { new Guid("a4e84922-0b61-4031-93ba-b074e85f7197"), null, null, new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"), new Guid("7c30c0cd-f8df-48b4-9eba-8c56c7c185c8"), true, "Miền Nam, HCM, Q1" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_BranchId",
                table: "Addresses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_EmployeeId",
                table: "Addresses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name",
                table: "Branches",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BranchProducts_BranchId",
                table: "BranchProducts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_CustomerId",
                table: "Credentials",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Username",
                table: "Employees",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherId",
                table: "Orders",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_Code",
                table: "Vouchers",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BranchProducts");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
