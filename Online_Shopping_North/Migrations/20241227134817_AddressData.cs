using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping_North.Migrations
{
    /// <inheritdoc />
    public partial class AddressData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Hải Phòng", "Bac" },
                    { new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Quảng Ninh", "Bac" },
                    { new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Phú Thọ", "Bac" },
                    { new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Thừa-Thiên Huế", "Trung" },
                    { new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Hải Dương", "Bac" },
                    { new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Quảng Trị", "Trung" },
                    { new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Đồng Nai", "Nam" },
                    { new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Long An", "Nam" },
                    { new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Quảng Nam", "Trung" },
                    { new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Bình Dương", "Nam" },
                    { new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Bà Rịa Vũng Tàu", "Nam" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("83ba4e24-9a0f-4c6a-b822-29a1eb5f4d3f"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Hoàn Kiếm" },
                    { new Guid("b051a4a6-66ba-4220-95e4-59abd37d4e0b"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Đống Đa" },
                    { new Guid("033fbf94-3a48-4bec-a1d1-13f0dcca7ff2"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Lê Chân" },
                    { new Guid("05e87ab6-1238-412d-9d93-88902310ee89"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Trảng Bom" },
                    { new Guid("1bc7754e-bacc-484f-b5cc-7e2df41f1f30"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Hạ Long" },
                    { new Guid("1be59787-ee81-4824-b7e1-766e71fffa6b"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Uông Bí" },
                    { new Guid("1f461ef4-7dbc-4f7c-b51e-a16a5cfca7d3"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Gia Lộc" },
                    { new Guid("24de8ac0-247f-40e4-b472-1171b56c1e74"), new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Hội An" },
                    { new Guid("283942d0-07a6-44c4-a8e3-af3372c4f4d7"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Thị xã Bà Rịa" },
                    { new Guid("289f3f8b-7b77-491e-9892-043cee73f0a3"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Cẩm Phả" },
                    { new Guid("2f657b93-1d8b-4024-bfd9-827009d98c67"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Hải Dương" },
                    { new Guid("412da1ea-5f1b-4aa3-9c13-cf5d557b59e3"), new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"), "Móng Cái" },
                    { new Guid("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"), new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"), "Chí Linh" },
                    { new Guid("4b1dd408-19c8-40cc-a39a-02ecaf53cfbe"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Phong Điền" },
                    { new Guid("57fa0862-5fc6-4e0b-bc55-b8dda3860fc8"), new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Đức Huệ" },
                    { new Guid("5e9c531c-cf58-4f0b-a447-d1a1cfd2b1b6"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Phú Vang" },
                    { new Guid("603cf681-062c-4378-89d0-a534ab196661"), new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Cam Lộ" },
                    { new Guid("8c132e18-9710-402f-a5c0-7ad8dd90311b"), new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"), "Tam Kỳ" },
                    { new Guid("8f23aec1-ad93-4170-8493-b388da9ec33e"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Tân Uyên" },
                    { new Guid("90c585ba-5d16-4406-ad97-41c34732ccd3"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Long Thành" },
                    { new Guid("91d1c4dd-f364-412d-a7a2-8857f4b8a9c9"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Thanh Thủy" },
                    { new Guid("93811fdc-4e5e-4e92-87dd-91650cfe357a"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Biên Hòa" },
                    { new Guid("9744402f-34c1-4555-baff-9450ab73303a"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Hồng Bàng" },
                    { new Guid("9d442ba3-12d2-4845-9a9c-92d88979bd96"), new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"), "Tân An" },
                    { new Guid("a9e0ff46-2f5c-463a-a793-08e9a533900c"), new Guid("3321ed88-441b-4121-9ead-e154544185e1"), "Hương Thủy" },
                    { new Guid("aab01227-c628-4ad0-a7c3-69ce33712109"), new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"), "Hải Lăng" },
                    { new Guid("aabecff8-16d3-4298-839b-c5ec84ae49a3"), new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"), "Kiến An" },
                    { new Guid("ab041f51-dcf0-4071-b1ae-6ebeaf3c4840"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Châu Đốc" },
                    { new Guid("af5c6bad-cba1-459c-aacc-e31438f4ba31"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Thủ Dầu Một" },
                    { new Guid("c56546a3-e495-4662-a7f4-9196eccbdbf7"), new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"), "Dĩ An" },
                    { new Guid("db4e7867-1156-44d9-825b-8f42ae9712fe"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Yên Lập" },
                    { new Guid("e60650de-7772-49d5-ac72-81d3bfa774d4"), new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"), "Long Khánh" },
                    { new Guid("eef65a95-e294-46a3-828f-5e44ca4b2c77"), new Guid("2df88e32-3919-494d-b489-dbf4258fc245"), "Hạ Hòa" },
                    { new Guid("f6220d40-db0b-4be5-ada4-5996bab22cd0"), new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"), "Xuyên Mộc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("033fbf94-3a48-4bec-a1d1-13f0dcca7ff2"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("05e87ab6-1238-412d-9d93-88902310ee89"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("1bc7754e-bacc-484f-b5cc-7e2df41f1f30"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("1be59787-ee81-4824-b7e1-766e71fffa6b"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("1f461ef4-7dbc-4f7c-b51e-a16a5cfca7d3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("24de8ac0-247f-40e4-b472-1171b56c1e74"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("283942d0-07a6-44c4-a8e3-af3372c4f4d7"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("289f3f8b-7b77-491e-9892-043cee73f0a3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("2f657b93-1d8b-4024-bfd9-827009d98c67"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("412da1ea-5f1b-4aa3-9c13-cf5d557b59e3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("4af1f7fa-cd35-4940-b2f9-8811ca9a2b75"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("4b1dd408-19c8-40cc-a39a-02ecaf53cfbe"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("57fa0862-5fc6-4e0b-bc55-b8dda3860fc8"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5e9c531c-cf58-4f0b-a447-d1a1cfd2b1b6"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("603cf681-062c-4378-89d0-a534ab196661"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("83ba4e24-9a0f-4c6a-b822-29a1eb5f4d3f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("8c132e18-9710-402f-a5c0-7ad8dd90311b"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("8f23aec1-ad93-4170-8493-b388da9ec33e"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("90c585ba-5d16-4406-ad97-41c34732ccd3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("91d1c4dd-f364-412d-a7a2-8857f4b8a9c9"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("93811fdc-4e5e-4e92-87dd-91650cfe357a"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9744402f-34c1-4555-baff-9450ab73303a"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9d442ba3-12d2-4845-9a9c-92d88979bd96"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a9e0ff46-2f5c-463a-a793-08e9a533900c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("aab01227-c628-4ad0-a7c3-69ce33712109"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("aabecff8-16d3-4298-839b-c5ec84ae49a3"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("ab041f51-dcf0-4071-b1ae-6ebeaf3c4840"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("af5c6bad-cba1-459c-aacc-e31438f4ba31"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("b051a4a6-66ba-4220-95e4-59abd37d4e0b"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("c56546a3-e495-4662-a7f4-9196eccbdbf7"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("db4e7867-1156-44d9-825b-8f42ae9712fe"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("e60650de-7772-49d5-ac72-81d3bfa774d4"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("eef65a95-e294-46a3-828f-5e44ca4b2c77"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("f6220d40-db0b-4be5-ada4-5996bab22cd0"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0bdea930-da3d-40c6-97cd-b3969f8014c7"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("14b0bd4d-27af-496e-aa9c-3e1d532f5038"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2df88e32-3919-494d-b489-dbf4258fc245"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3321ed88-441b-4121-9ead-e154544185e1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4a486645-052b-4a56-bb36-75c7e876ae2d"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a41ba56a-b53b-42f6-8c56-04dcbbde7905"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("acd51ba8-d6e3-4110-831e-5147f8fe2c96"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ad453439-a309-42a3-917c-d6aaa67ac9ca"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("aec5e588-017c-4da1-91e8-b8bc1888056e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b40f6c23-15f7-460c-8f94-fdcbe33cda68"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bbf96ba4-7836-4c53-af1a-e3e572f31ebf"));
        }
    }
}
