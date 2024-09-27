using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SearchLimitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblSearchlimit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchLimitCount = table.Column<int>(type: "int", nullable: false),
                    SearchLimitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSearchlimit", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblSearchlimit");
        }
    }
}
