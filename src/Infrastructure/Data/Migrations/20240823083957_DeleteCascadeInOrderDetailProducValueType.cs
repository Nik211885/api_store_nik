using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeInOrderDetailProducValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailProductValueType_OrderDetails_OrderDetailId",
                table: "OrderDetailProductValueType");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailProductValueType_OrderDetails_OrderDetailId",
                table: "OrderDetailProductValueType",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailProductValueType_OrderDetails_OrderDetailId",
                table: "OrderDetailProductValueType");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailProductValueType_OrderDetails_OrderDetailId",
                table: "OrderDetailProductValueType",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id");
        }
    }
}
