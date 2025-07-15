using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalId",
                table: "CollectedCards",
                newName: "SetId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectedCards_ExternalId_PokedexId",
                table: "CollectedCards",
                newName: "IX_CollectedCards_SetId_PokedexId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetId",
                table: "CollectedCards",
                newName: "ExternalId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectedCards_SetId_PokedexId",
                table: "CollectedCards",
                newName: "IX_CollectedCards_ExternalId_PokedexId");
        }
    }
}
