using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XmlValidador.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarNcmItemNotaFiscal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ncm",
                table: "ItensNotaFiscal",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ncm",
                table: "ItensNotaFiscal");
        }
    }
}
