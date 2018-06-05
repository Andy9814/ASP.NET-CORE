using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ASPNETExercises.Migrations
{
    public partial class addTray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tray",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Timer = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    TotalCalories = table.Column<int>(nullable: false),
                    TotalCholesterol = table.Column<int>(nullable: false),
                    TotalFat = table.Column<decimal>(nullable: false),
                    TotalFibre = table.Column<int>(nullable: false),
                    TotalProtein = table.Column<int>(nullable: false),
                    TotalSalt = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tray", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrayItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuItemId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Timer = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    TrayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrayItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrayItem_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrayItem_Tray_TrayId",
                        column: x => x.TrayId,
                        principalTable: "Tray",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrayItem_MenuItemId",
                table: "TrayItem",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TrayItem_TrayId",
                table: "TrayItem",
                column: "TrayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrayItem");

            migrationBuilder.DropTable(
                name: "Tray");
        }
    }
}
