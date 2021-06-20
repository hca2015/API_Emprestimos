using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class CorrecaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDENTITYID",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "Usuario",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "IDENTITYID",
                table: "Usuario",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }
    }
}
