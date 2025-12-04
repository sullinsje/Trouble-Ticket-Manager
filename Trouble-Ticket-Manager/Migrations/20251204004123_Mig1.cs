using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trouble_Ticket_Manager.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Building = table.Column<string>(type: "TEXT", nullable: true),
                    Room = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    ServiceTag = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    UnderWarranty = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.AssetTag);
                    table.ForeignKey(
                        name: "FK_Computers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsResolved = table.Column<bool>(type: "INTEGER", nullable: true),
                    ChargerGiven = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketComputers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IssueDescription = table.Column<string>(type: "TEXT", nullable: false),
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerAssetTag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComputers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketComputers_Computers_ComputerAssetTag",
                        column: x => x.ComputerAssetTag,
                        principalTable: "Computers",
                        principalColumn: "AssetTag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketComputers_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computers_UserId",
                table: "Computers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComputers_ComputerAssetTag",
                table: "TicketComputers",
                column: "ComputerAssetTag");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComputers_TicketId",
                table: "TicketComputers",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ContactId",
                table: "Tickets",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketComputers");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
