using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class TransactionHistoryAddedStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReturned",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transactions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.AddColumn<bool>(
                name: "isReturned",
                table: "Transactions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
