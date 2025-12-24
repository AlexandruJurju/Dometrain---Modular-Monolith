using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverBooks.OrderProcessing.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Orders");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    BillingAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BillingAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_Street1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingAddress_Street2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Street1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Street2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Orders",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Orders",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Orders");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Orders");
        }
    }
}
