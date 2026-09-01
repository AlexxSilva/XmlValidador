using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlValidador.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarHistoricoValidacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricosValidacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaFiscalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valido = table.Column<bool>(type: "bit", nullable: false),
                    Xml = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosValidacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosValidacao_NotasFiscais_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalTable: "NotasFiscais",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosValidacao_NotaFiscalId",
                table: "HistoricosValidacao",
                column: "NotaFiscalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosValidacao");
        }
    }
}
