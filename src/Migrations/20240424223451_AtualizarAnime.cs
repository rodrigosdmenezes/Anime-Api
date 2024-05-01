using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "Animes",
                newName: "Resumo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Animes",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resumo",
                table: "Animes",
                newName: "Resume");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Animes",
                newName: "Name");
        }
    }
}
