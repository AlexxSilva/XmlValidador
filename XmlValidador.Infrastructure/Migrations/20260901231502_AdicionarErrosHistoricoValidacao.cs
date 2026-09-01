using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlValidador.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarErrosHistoricoValidacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrosHistoricoValidacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoricoValidacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Severidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrosHistoricoValidacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrosHistoricoValidacao_HistoricosValidacao_HistoricoValidacaoId",
                        column: x => x.HistoricoValidacaoId,
                        principalTable: "HistoricosValidacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrosHistoricoValidacao_HistoricoValidacaoId",
                table: "ErrosHistoricoValidacao",
                column: "HistoricoValidacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrosHistoricoValidacao");
        }
    }
}
