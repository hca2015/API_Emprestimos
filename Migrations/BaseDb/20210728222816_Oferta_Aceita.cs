using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class Oferta_Aceita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ACEITO",
                table: "OfertaEmprestimo",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACEITO",
                table: "OfertaEmprestimo");
        }
    }
}
