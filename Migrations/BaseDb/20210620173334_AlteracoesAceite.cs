using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class AlteracoesAceite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CANCELADO",
                table: "OfertaEmprestimo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CREDORUSUARIOID",
                table: "AceiteEmprestimo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VALORINICIAL",
                table: "AceiteEmprestimo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_AceiteEmprestimo_CREDORUSUARIOID",
                table: "AceiteEmprestimo",
                column: "CREDORUSUARIOID");

            migrationBuilder.CreateIndex(
                name: "IX_AceiteEmprestimo_REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo",
                column: "REQUERENTEUSUARIOID");

            migrationBuilder.AddForeignKey(
                name: "FK_AceiteEmprestimo_Usuario_CREDORUSUARIOID",
                table: "AceiteEmprestimo",
                column: "CREDORUSUARIOID",
                principalTable: "Usuario",
                principalColumn: "USUARIOID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AceiteEmprestimo_Usuario_REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo",
                column: "REQUERENTEUSUARIOID",
                principalTable: "Usuario",
                principalColumn: "USUARIOID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AceiteEmprestimo_Usuario_CREDORUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropForeignKey(
                name: "FK_AceiteEmprestimo_Usuario_REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropIndex(
                name: "IX_AceiteEmprestimo_CREDORUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropIndex(
                name: "IX_AceiteEmprestimo_REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropColumn(
                name: "CANCELADO",
                table: "OfertaEmprestimo");

            migrationBuilder.DropColumn(
                name: "CREDORUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropColumn(
                name: "REQUERENTEUSUARIOID",
                table: "AceiteEmprestimo");

            migrationBuilder.DropColumn(
                name: "VALORINICIAL",
                table: "AceiteEmprestimo");
        }
    }
}
