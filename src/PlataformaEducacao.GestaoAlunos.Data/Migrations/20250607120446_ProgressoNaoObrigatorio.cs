﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducacao.GestaoAlunos.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgressoNaoObrigatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HistoricoAprendizado_Progresso",
                table: "Matriculas",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HistoricoAprendizado_Progresso",
                table: "Matriculas",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);
        }
    }
}
