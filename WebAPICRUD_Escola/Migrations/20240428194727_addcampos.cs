using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPICRUD_Escola.Migrations
{
    /// <inheritdoc />
    public partial class addcampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Escolas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Escolas");
        }
    }
}
