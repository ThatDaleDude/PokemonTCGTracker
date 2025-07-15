using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddPokedexIdAndUniqueConstraintToCollectedCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CollectedCards_ExternalId",
                table: "CollectedCards");

            migrationBuilder.AddColumn<int>(
                name: "PokedexId",
                table: "CollectedCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CollectedCards_ExternalId_PokedexId",
                table: "CollectedCards",
                columns: new[] { "ExternalId", "PokedexId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CollectedCards_ExternalId_PokedexId",
                table: "CollectedCards");

            migrationBuilder.DropColumn(
                name: "PokedexId",
                table: "CollectedCards");

            migrationBuilder.CreateIndex(
                name: "IX_CollectedCards_ExternalId",
                table: "CollectedCards",
                column: "ExternalId",
                unique: true);
        }
    }
}
