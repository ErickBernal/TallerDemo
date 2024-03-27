using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TallerApi.Data.migrations
{
    /// <inheritdoc />
    public partial class initiaD2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Country_IdCountryNavigationIdCountry",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipality_Department_IdDepartmentNavigationIdDepartment",
                table: "Municipality");

            migrationBuilder.DropColumn(
                name: "IdDepartment",
                table: "Municipality");

            migrationBuilder.DropColumn(
                name: "IdCountry",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "IdDepartmentNavigationIdDepartment",
                table: "Municipality",
                newName: "IdDepartment1");

            migrationBuilder.RenameIndex(
                name: "IX_Municipality_IdDepartmentNavigationIdDepartment",
                table: "Municipality",
                newName: "IX_Municipality_IdDepartment1");

            migrationBuilder.RenameColumn(
                name: "IdCountryNavigationIdCountry",
                table: "Department",
                newName: "IdCountry1");

            migrationBuilder.RenameIndex(
                name: "IX_Department_IdCountryNavigationIdCountry",
                table: "Department",
                newName: "IX_Department_IdCountry1");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Country_IdCountry1",
                table: "Department",
                column: "IdCountry1",
                principalTable: "Country",
                principalColumn: "IdCountry");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipality_Department_IdDepartment1",
                table: "Municipality",
                column: "IdDepartment1",
                principalTable: "Department",
                principalColumn: "IdDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Country_IdCountry1",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipality_Department_IdDepartment1",
                table: "Municipality");

            migrationBuilder.RenameColumn(
                name: "IdDepartment1",
                table: "Municipality",
                newName: "IdDepartmentNavigationIdDepartment");

            migrationBuilder.RenameIndex(
                name: "IX_Municipality_IdDepartment1",
                table: "Municipality",
                newName: "IX_Municipality_IdDepartmentNavigationIdDepartment");

            migrationBuilder.RenameColumn(
                name: "IdCountry1",
                table: "Department",
                newName: "IdCountryNavigationIdCountry");

            migrationBuilder.RenameIndex(
                name: "IX_Department_IdCountry1",
                table: "Department",
                newName: "IX_Department_IdCountryNavigationIdCountry");

            migrationBuilder.AddColumn<int>(
                name: "IdDepartment",
                table: "Municipality",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCountry",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Country_IdCountryNavigationIdCountry",
                table: "Department",
                column: "IdCountryNavigationIdCountry",
                principalTable: "Country",
                principalColumn: "IdCountry");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipality_Department_IdDepartmentNavigationIdDepartment",
                table: "Municipality",
                column: "IdDepartmentNavigationIdDepartment",
                principalTable: "Department",
                principalColumn: "IdDepartment");
        }
    }
}
