using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trouble_Ticket_Manager.Migrations
{
    /// <inheritdoc />
    public partial class WarrantyColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UnderWarranty",
                table: "Computers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnderWarranty",
                table: "Computers");
        }
    }
}
