using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_final_test.Migrations
{
    public partial class AddSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaccinationSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccinationDates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinationSchedules_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchedules_VaccineId",
                table: "VaccinationSchedules",
                column: "VaccineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccinationSchedules");
        }
    }
}
