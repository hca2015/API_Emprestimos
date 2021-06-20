using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class CriadoOferta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CRIADO",
                table: "OfertaEmprestimo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRIADO",
                table: "OfertaEmprestimo");
        }
    }
}
