using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class UniquesUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CPF",
                table: "Usuario",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EMAIL",
                table: "Usuario",
                column: "EMAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_CPF",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_EMAIL",
                table: "Usuario");
        }
    }
}
