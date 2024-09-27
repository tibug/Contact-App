using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnSearchLinitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tblSearchlimit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "tblSearchlimit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tblSearchlimit");

            migrationBuilder.DropColumn(
                name: "password",
                table: "tblSearchlimit");
        }
    }
}
