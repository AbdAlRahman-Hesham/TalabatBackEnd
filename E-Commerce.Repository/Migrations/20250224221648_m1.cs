using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.Repository.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryMethodId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ProductId = table.Column<int>(type: "int", nullable: false),
                    Product_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Starbucks" },
                    { 2, "Costa" },
                    { 3, "Cilantro" },
                    { 4, "TBS" },
                    { 5, "On The Run" },
                    { 6, "Caribou" },
                    { 7, "Krispy Kreme" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Frappuccino" },
                    { 2, "Latte" },
                    { 3, "Mocha" },
                    { 4, "Macchiato" },
                    { 5, "Matcha" },
                    { 6, "Cake" },
                    { 7, "Donuts" },
                    { 8, "Salad" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "Cost", "DeliveryTime", "Description", "ShortName" },
                values: new object[,]
                {
                    { 1, 10m, "1-2 Days", "Fastest delivery time", "UPS1" },
                    { 2, 5m, "2-5 Days", "Get it within 5 days", "UPS2" },
                    { 3, 2m, "5-10 Days", "Slower but cheap", "UPS3" },
                    { 4, 0m, "1-2 Weeks", "Free! You get what you pay for", "FREE" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Name", "PictureUrl", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit...", "Double Caramel Frappuccino", "images/products/sb-ang1.png", 200m },
                    { 2, 1, 1, "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.", "White Chocolate Mocha Frappuccino", "images/products/sb-ang2.png", 150m },
                    { 3, 1, 2, "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", "Iced Cafe Latte", "images/products/sb-core1.png", 180m },
                    { 4, 2, 3, "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", "White Chocolate Mocha", "images/products/sb-core2.png", 300m },
                    { 5, 1, 4, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit...", "Iced Caramel Macchiato", "images/products/sb-react1.png", 250m },
                    { 6, 5, 4, "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.", "Hot Caramel Macchiato", "images/products/sb-ts1.png", 120m },
                    { 7, 2, 5, "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero.", "Iced Matcha Latte", "images/products/hat-core1.png", 10m },
                    { 8, 1, 6, "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", "Honey Cake", "images/products/hat-react1.png", 8m },
                    { 9, 1, 6, "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero.", "Blueberry Cheesecake", "images/products/hat-react2.png", 15m },
                    { 10, 3, 7, "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.", "Glazed Donuts", "images/products/glove-code1.png", 18m },
                    { 11, 1, 7, "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", "Greek Salad", "images/products/glove-code2.png", 15m },
                    { 12, 4, 4, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", "Iced Black Tea Lemonade", "images/products/glove-react1.png", 16m },
                    { 13, 4, 4, "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", "Iced London Fog Tea Latte", "images/products/glove-react2.png", 14m },
                    { 14, 6, 3, "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", "Turkey Bacon, Cheddar & Egg White Sandwich", "images/products/boot-redis1.png", 250m },
                    { 15, 2, 3, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.", "Core Red Boots", "images/products/boot-core2.png", 189.99m },
                    { 16, 2, 3, "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", "Double-Smoked Bacon, Cheddar & Egg Sandwich", "images/products/boot-core1.png", 199.99m },
                    { 17, 1, 3, "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.", "Iced Chai Tea Latte with Oleato Golden Foam", "images/products/boot-ang2.png", 150m },
                    { 18, 1, 3, "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.", "Dragon Drink® Starbucks Refreshers® Beverage with Oleato Golden Foam", "images/products/boot-ang1.png", 180m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");
        }
    }
}
