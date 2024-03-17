using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HBManager.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ColorIndex = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ColorIndex = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Retailers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ColorIndex = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retailers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListCatalogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListCatalogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastOnlineDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    ShoppingListCatalogID = table.Column<int>(type: "int", nullable: false),
                    RetailerID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_Retailers_RetailerID",
                        column: x => x.RetailerID,
                        principalTable: "Retailers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_ShoppingListCatalogs_ShoppingListCatalogID",
                        column: x => x.ShoppingListCatalogID,
                        principalTable: "ShoppingListCatalogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ShoppingListID = table.Column<int>(type: "int", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    IsFavorited = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ShoppingLists_ShoppingListID",
                        column: x => x.ShoppingListID,
                        principalTable: "ShoppingLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerProduct",
                columns: table => new
                {
                    ConsumersID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerProduct", x => new { x.ConsumersID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_ConsumerProduct_Consumers_ConsumersID",
                        column: x => x.ConsumersID,
                        principalTable: "Consumers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumerProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "Color", "ColorIndex", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, "", 0, "", "Obst" },
                    { 2, "", 0, "", "Gemüse" },
                    { 3, "", 0, "", "Fleisch & Fisch" },
                    { 4, "", 0, "", "Fleisch (Bio)" },
                    { 5, "", 0, "", "Käse" },
                    { 6, "", 0, "", "Eier & Milch" },
                    { 7, "", 0, "", "Spezialitäten & Feinkost" },
                    { 8, "", 0, "", "Gebäck" },
                    { 9, "", 0, "", "Heißgetränke" },
                    { 10, "", 0, "", "Nudeln, Reis & Co" },
                    { 11, "", 0, "", "Saucen, Öle & Gewürze" },
                    { 12, "", 0, "", "Fertiggerichte & Konserven" },
                    { 13, "", 0, "", "Tiefkühl" },
                    { 14, "", 0, "", "Backen & Dessert" },
                    { 15, "", 0, "", "Süßes & Salziges" },
                    { 16, "", 0, "", "Getränke" },
                    { 17, "", 0, "", "Getränke (alkoholisch)" },
                    { 18, "", 0, "", "Drogerie" },
                    { 19, "", 0, "", "Baby & Kind" },
                    { 20, "", 0, "", "Haushalt" },
                    { 21, "", 0, "", "Tier" },
                    { 22, "", 0, "", "Kleidung" },
                    { 23, "", 0, "", "Elektronik" },
                    { 24, "", 0, "", "Wurst & Aufstriche" },
                    { 512, "", 0, "", "Sonstige" }
                });

            migrationBuilder.InsertData(
                table: "Retailers",
                columns: new[] { "ID", "Color", "ColorIndex", "ImageURL", "Name" },
                values: new object[,]
                {
                    { 1, "", 0, "", "Aldi Nord" },
                    { 2, "", 0, "", "Aldi Süd" },
                    { 3, "", 0, "", "DM" },
                    { 4, "", 0, "", "EDEKA" },
                    { 5, "", 0, "", "Globus" },
                    { 6, "", 0, "", "Kaufland" },
                    { 7, "", 0, "", "Lidl" },
                    { 8, "", 0, "", "Metro" },
                    { 9, "", 0, "", "Müller" },
                    { 10, "", 0, "", "Netto" },
                    { 11, "", 0, "", "Norma" },
                    { 12, "", 0, "", "Penny" },
                    { 13, "", 0, "", "Picnic" },
                    { 14, "", 0, "", "Real" },
                    { 15, "", 0, "", "Rewe" },
                    { 16, "", 0, "", "Rossmann" },
                    { 17, "", 0, "", "Spar" },
                    { 18, "", 0, "", "Trinkgut" },
                    { 19, "", 0, "", "Trink & Spare" },
                    { 512, "", 0, "", "Sonstige" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CreationDate", "LastOnlineDate", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1276), new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1277), "helmut", "$2a$11$cC2HGDVdEjiqn0i/zFD16.phjapeXJDtQo0LlcoEmI5/aGYkEdCVq", "User" },
                    { 2, new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1280), new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1281), "helmut2", "$2a$11$oxFGwhEGy.7meEYttK9jwusPG9Ydx7v3WsASKdllv2FDTWl30jqxm", "User" },
                    { 3, new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1284), new DateTime(2023, 9, 10, 3, 14, 42, 84, DateTimeKind.Local).AddTicks(1285), "Ingeborg-HHB", "$2a$11$oxFGwhEGy.7meEYttK9jwusPG9Ydx7v3WsASKdllv2FDTWl30jqxm", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerProduct_ProductsID",
                table: "ConsumerProduct",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingListID",
                table: "Products",
                column: "ShoppingListID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_RetailerID",
                table: "ShoppingLists",
                column: "RetailerID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_ShoppingListCatalogID",
                table: "ShoppingLists",
                column: "ShoppingListCatalogID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerProduct");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Retailers");

            migrationBuilder.DropTable(
                name: "ShoppingListCatalogs");
        }
    }
}
