using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promoter.Infra.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "promoter-api");

            migrationBuilder.CreateTable(
                name: "Promoters",
                schema: "promoter-api",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CorporateReason = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Fantasy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promoters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promoters_Cnpj",
                schema: "promoter-api",
                table: "Promoters",
                column: "Cnpj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promoters",
                schema: "promoter-api");
        }
    }
}
