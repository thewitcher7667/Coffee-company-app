using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class addresetitemsandcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "CoffeItems");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "CoffeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsBuyed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoffeItemId = table.Column<int>(type: "int", nullable: false),
                    ResetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsBuyed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsBuyed_CoffeItems_CoffeItemId",
                        column: x => x.CoffeItemId,
                        principalTable: "CoffeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsBuyed_Resets_ResetId",
                        column: x => x.ResetId,
                        principalTable: "Resets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeItems_CategoryId",
                table: "CoffeItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsBuyed_CoffeItemId",
                table: "ItemsBuyed",
                column: "CoffeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsBuyed_ResetId",
                table: "ItemsBuyed",
                column: "ResetId");

            migrationBuilder.CreateIndex(
                name: "IX_Resets_UserId",
                table: "Resets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeItems_CategoryItems_CategoryId",
                table: "CoffeItems",
                column: "CategoryId",
                principalTable: "CategoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeItems_CategoryItems_CategoryId",
                table: "CoffeItems");

            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.DropTable(
                name: "ItemsBuyed");

            migrationBuilder.DropTable(
                name: "Resets");

            migrationBuilder.DropIndex(
                name: "IX_CoffeItems_CategoryId",
                table: "CoffeItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CoffeItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CoffeItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
