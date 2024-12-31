using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeOnBranchAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }
    }
}
