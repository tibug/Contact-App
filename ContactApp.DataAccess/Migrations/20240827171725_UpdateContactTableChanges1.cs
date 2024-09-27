using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactTableChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyid = table.Column<string>(name: "company_id", type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailscore = table.Column<string>(name: "email_score", type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    workphone = table.Column<string>(name: "work_phone", type: "nvarchar(max)", nullable: true),
                    leadlocation = table.Column<string>(name: "lead_location", type: "nvarchar(max)", nullable: false),
                    leaddivison = table.Column<string>(name: "lead_divison", type: "nvarchar(max)", nullable: true),
                    leadtitles = table.Column<string>(name: "lead_titles", type: "nvarchar(max)", nullable: true),
                    decisionmakingpower = table.Column<string>(name: "decision_making_power", type: "nvarchar(max)", nullable: true),
                    senioritylevel = table.Column<string>(name: "seniority_level", type: "nvarchar(max)", nullable: true),
                    linkedinurl = table.Column<string>(name: "linkedin_url", type: "nvarchar(max)", nullable: true),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pastcompanies = table.Column<string>(name: "past_companies", type: "nvarchar(max)", nullable: true),
                    companyname = table.Column<string>(name: "company_name", type: "nvarchar(max)", nullable: true),
                    companysize = table.Column<string>(name: "company_size", type: "nvarchar(max)", nullable: true),
                    companywebsite = table.Column<string>(name: "company_website", type: "nvarchar(max)", nullable: true),
                    companyphonenumbers = table.Column<string>(name: "company_phone_numbers", type: "nvarchar(max)", nullable: true),
                    companylocationtext = table.Column<string>(name: "company_location_text", type: "nvarchar(max)", nullable: true),
                    companytype = table.Column<string>(name: "company_type", type: "nvarchar(max)", nullable: true),
                    companyfunction = table.Column<string>(name: "company_function", type: "nvarchar(max)", nullable: true),
                    companysector = table.Column<string>(name: "company_sector", type: "nvarchar(max)", nullable: true),
                    companyindustry = table.Column<string>(name: "company_industry", type: "nvarchar(max)", nullable: true),
                    companyfoundedat = table.Column<string>(name: "company_founded_at", type: "nvarchar(max)", nullable: true),
                    companyfundingrange = table.Column<string>(name: "company_funding_range", type: "nvarchar(max)", nullable: true),
                    revenuerange = table.Column<string>(name: "revenue_range", type: "nvarchar(max)", nullable: true),
                    ebitdarange = table.Column<string>(name: "ebitda_range", type: "nvarchar(max)", nullable: true),
                    companyfacebookpage = table.Column<string>(name: "company_facebook_page", type: "nvarchar(max)", nullable: true),
                    companytwitterpage = table.Column<string>(name: "company_twitter_page", type: "nvarchar(max)", nullable: true),
                    companylinkedinpage = table.Column<string>(name: "company_linkedin_page", type: "nvarchar(max)", nullable: true),
                    companysiccode = table.Column<string>(name: "company_sic_code", type: "nvarchar(max)", nullable: true),
                    companynaicscode = table.Column<string>(name: "company_naics_code", type: "nvarchar(max)", nullable: true),
                    companydescription = table.Column<string>(name: "company_description", type: "nvarchar(max)", nullable: true),
                    companysizekey = table.Column<string>(name: "company_size_key", type: "nvarchar(max)", nullable: true),
                    companyprofileimageurl = table.Column<string>(name: "company_profile_image_url", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSearchlimit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchLimitCount = table.Column<int>(type: "int", nullable: false),
                    SearchLimitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchBy = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSearchlimit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUserExport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportCount = table.Column<int>(type: "int", nullable: false),
                    ExportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExportBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserExport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExportTodayLimit = table.Column<int>(type: "int", nullable: false),
                    PerExportLimit = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    searchlimit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblContacts");

            migrationBuilder.DropTable(
                name: "tblSearchlimit");

            migrationBuilder.DropTable(
                name: "tblUserExport");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
