using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPICRUD_Escola.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumSalas",
                table: "Escolas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumSalas",
                table: "Escolas");
        }
    }
}



