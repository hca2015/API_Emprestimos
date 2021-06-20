using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Emprestimos.Migrations.BaseDb
{
    public partial class ModelosIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    USUARIOID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDENTITYID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ENDERECO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BAIRRO = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CIDADE = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ESTADO = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.USUARIOID);
                });

            migrationBuilder.CreateTable(
                name: "PedidoEmprestimo",
                columns: table => new
                {
                    PEDIDOID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIOID = table.Column<int>(type: "int", nullable: true),
                    VALOR = table.Column<double>(type: "float", nullable: false),
                    CRIADO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoEmprestimo", x => x.PEDIDOID);
                    table.ForeignKey(
                        name: "FK_PedidoEmprestimo_Usuario_USUARIOID",
                        column: x => x.USUARIOID,
                        principalTable: "Usuario",
                        principalColumn: "USUARIOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfertaEmprestimo",
                columns: table => new
                {
                    OFERTAID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIOID = table.Column<int>(type: "int", nullable: true),
                    PEDIDOID = table.Column<int>(type: "int", nullable: false),
                    TAXA = table.Column<double>(type: "float", nullable: false),
                    TEMPO = table.Column<int>(type: "int", nullable: false),
                    TIPOTEMPO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaEmprestimo", x => x.OFERTAID);
                    table.ForeignKey(
                        name: "FK_OfertaEmprestimo_PedidoEmprestimo_PEDIDOID",
                        column: x => x.PEDIDOID,
                        principalTable: "PedidoEmprestimo",
                        principalColumn: "PEDIDOID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertaEmprestimo_Usuario_USUARIOID",
                        column: x => x.USUARIOID,
                        principalTable: "Usuario",
                        principalColumn: "USUARIOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AceiteEmprestimo",
                columns: table => new
                {
                    ACEITEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PEDIDOID = table.Column<int>(type: "int", nullable: true),
                    OFERTAID = table.Column<int>(type: "int", nullable: true),
                    ACEITO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TAXAFINAL = table.Column<double>(type: "float", nullable: false),
                    VALORFINAL = table.Column<double>(type: "float", nullable: false),
                    TEMPOFINAL = table.Column<double>(type: "float", nullable: false),
                    TIPOTEMPOFINAL = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AceiteEmprestimo", x => x.ACEITEID);
                    table.ForeignKey(
                        name: "FK_AceiteEmprestimo_OfertaEmprestimo_OFERTAID",
                        column: x => x.OFERTAID,
                        principalTable: "OfertaEmprestimo",
                        principalColumn: "OFERTAID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AceiteEmprestimo_PedidoEmprestimo_PEDIDOID",
                        column: x => x.PEDIDOID,
                        principalTable: "PedidoEmprestimo",
                        principalColumn: "PEDIDOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AceiteEmprestimo_OFERTAID",
                table: "AceiteEmprestimo",
                column: "OFERTAID");

            migrationBuilder.CreateIndex(
                name: "IX_AceiteEmprestimo_PEDIDOID",
                table: "AceiteEmprestimo",
                column: "PEDIDOID");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaEmprestimo_PEDIDOID",
                table: "OfertaEmprestimo",
                column: "PEDIDOID");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaEmprestimo_USUARIOID",
                table: "OfertaEmprestimo",
                column: "USUARIOID");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoEmprestimo_USUARIOID",
                table: "PedidoEmprestimo",
                column: "USUARIOID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AceiteEmprestimo");

            migrationBuilder.DropTable(
                name: "OfertaEmprestimo");

            migrationBuilder.DropTable(
                name: "PedidoEmprestimo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
