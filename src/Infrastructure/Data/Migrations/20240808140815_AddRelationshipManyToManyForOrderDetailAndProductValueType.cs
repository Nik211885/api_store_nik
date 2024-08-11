using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipManyToManyForOrderDetailAndProductValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Start",
                table: "Ratings",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "OrderDetailProductValueType",
                columns: table => new
                {
                    OrderDetailId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProductValueTypeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailProductValueType", x => new { x.ProductValueTypeId, x.OrderDetailId });
                    table.ForeignKey(
                        name: "FK_OrderDetailProductValueType_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetailProductValueType_ProductValueTypes_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "ProductValueTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailProductValueType_OrderDetailId",
                table: "OrderDetailProductValueType",
                column: "OrderDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailProductValueType");

            migrationBuilder.AlterColumn<float>(
                name: "Start",
                table: "Ratings",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
