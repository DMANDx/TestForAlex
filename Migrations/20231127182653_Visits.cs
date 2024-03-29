using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Avto1Test.Migrations
{
    public partial class Visits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserAgent = table.Column<string>(type: "longtext", nullable: true),
                    RemoteAddr = table.Column<string>(type: "longtext", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");
        }
    }
}
