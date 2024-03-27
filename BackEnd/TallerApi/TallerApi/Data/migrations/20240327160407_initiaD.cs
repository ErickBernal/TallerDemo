using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TallerApi.Data.migrations
{
    /// <inheritdoc />
    public partial class initiaD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    IdDepartment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdCountryNavigationIdCountry = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.IdDepartment);
                    table.ForeignKey(
                        name: "FK_Department_Country_IdCountryNavigationIdCountry",
                        column: x => x.IdCountryNavigationIdCountry,
                        principalTable: "Country",
                        principalColumn: "IdCountry");
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    IdMunicipality = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDepartment = table.Column<int>(type: "int", nullable: false),
                    IdDepartmentNavigationIdDepartment = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.IdMunicipality);
                    table.ForeignKey(
                        name: "FK_Municipality_Department_IdDepartmentNavigationIdDepartment",
                        column: x => x.IdDepartmentNavigationIdDepartment,
                        principalTable: "Department",
                        principalColumn: "IdDepartment");
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "IdCountry", "Name" },
                values: new object[,]
                {
                    { 1, "Guatemala" },
                    { 2, "El Salvador" },
                    { 3, "Velice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_IdCountryNavigationIdCountry",
                table: "Department",
                column: "IdCountryNavigationIdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_IdDepartmentNavigationIdDepartment",
                table: "Municipality",
                column: "IdDepartmentNavigationIdDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
