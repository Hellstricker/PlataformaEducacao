using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducacao.Gestao.Data.Migrations
{
    /// <inheritdoc />
    public partial class Finalizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Curso_CursoValor",
                table: "Matriculas",
                newName: "CursoValor");

            migrationBuilder.RenameColumn(
                name: "Curso_CursoTotalAulas",
                table: "Matriculas",
                newName: "CursoTotalAulas");

            migrationBuilder.RenameColumn(
                name: "Curso_CursoNome",
                table: "Matriculas",
                newName: "CursoNome");

            migrationBuilder.RenameColumn(
                name: "Curso_CursoId",
                table: "Matriculas",
                newName: "CursoId");

            migrationBuilder.AddColumn<decimal>(
                name: "CursoProgresso",
                table: "Matriculas",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AulasFinalizadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AulaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MatriculaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulasFinalizadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AulasFinalizadas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumeroCertificado = table.Column<Guid>(type: "TEXT", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NomeCurso = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificados_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AulasFinalizadas_MatriculaId",
                table: "AulasFinalizadas",
                column: "MatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificados_AlunoId",
                table: "Certificados",
                column: "AlunoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AulasFinalizadas");

            migrationBuilder.DropTable(
                name: "Certificados");

            migrationBuilder.DropColumn(
                name: "CursoProgresso",
                table: "Matriculas");

            migrationBuilder.RenameColumn(
                name: "CursoValor",
                table: "Matriculas",
                newName: "Curso_CursoValor");

            migrationBuilder.RenameColumn(
                name: "CursoTotalAulas",
                table: "Matriculas",
                newName: "Curso_CursoTotalAulas");

            migrationBuilder.RenameColumn(
                name: "CursoNome",
                table: "Matriculas",
                newName: "Curso_CursoNome");

            migrationBuilder.RenameColumn(
                name: "CursoId",
                table: "Matriculas",
                newName: "Curso_CursoId");
        }
    }
}
