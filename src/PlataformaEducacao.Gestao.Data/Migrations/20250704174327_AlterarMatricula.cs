using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducacao.Gestao.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterarMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlunoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Curso_CursoId = table.Column<Guid>(type: "varchar(50)", nullable: false),
                    Curso_CursoNome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Curso_CursoValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Curso_CursoTotalAulas = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriculas");
        }
    }
}
