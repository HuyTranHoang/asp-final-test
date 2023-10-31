using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_final_test.Migrations
{
    public partial class AddTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 31, 2, 40, 50, 0, DateTimeKind.Unspecified), "Inactivated" },
                    { 2, new DateTime(2023, 10, 31, 3, 44, 12, 0, DateTimeKind.Unspecified), "Live-attenuated" },
                    { 3, new DateTime(2023, 10, 31, 4, 55, 23, 0, DateTimeKind.Unspecified), "Messenger RNA (mRNA)" },
                    { 4, new DateTime(2023, 10, 31, 5, 22, 34, 0, DateTimeKind.Unspecified), "Subunit, recombinant, polysaccharide, and conjugate" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
