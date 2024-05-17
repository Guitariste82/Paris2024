using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paris2024.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionforBugforpkonofferType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cart_CookieId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cart_IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "OfferType",
                columns: table => new
                {
                    OfferTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferType_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfferType_AllowedPerson = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferType", x => x.OfferTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_StatusId = table.Column<int>(type: "int", nullable: false),
                    Order_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Order_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Order_MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order_PaymentMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Order_IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Offer_Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Offer_Sport = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Offer_Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Offer_ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offer_UnitPrice = table.Column<double>(type: "float", nullable: false),
                    OfferTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_Offer_OfferType_OfferTypeId",
                        column: x => x.OfferTypeId,
                        principalTable: "OfferType",
                        principalColumn: "OfferTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartItem_Quantity = table.Column<int>(type: "int", nullable: false),
                    CartItem_UnitPrice = table.Column<double>(type: "float", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItem_Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderItem_UnitPrice = table.Column<double>(type: "float", nullable: false),
                    OrderItem_Key2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItem_QrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_OfferId",
                table: "CartItem",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferTypeId",
                table: "Offer",
                column: "OfferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OfferId",
                table: "OrderItem",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OfferType");
        }
    }
}
