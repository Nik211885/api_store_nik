using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditContrainInOrderDetailProductValueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailProductValueType_ProductValueTypes_OrderDetailId",
                table: "OrderDetailProductValueType");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailProductValueType_ProductValueTypes_ProductValueTypeId",
                table: "OrderDetailProductValueType",
                column: "ProductValueTypeId",
                principalTable: "ProductValueTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailProductValueType_ProductValueTypes_ProductValueTypeId",
                table: "OrderDetailProductValueType");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailProductValueType_ProductValueTypes_OrderDetailId",
                table: "OrderDetailProductValueType",
                column: "OrderDetailId",
                principalTable: "ProductValueTypes",
                principalColumn: "Id");
        }
    }
}
