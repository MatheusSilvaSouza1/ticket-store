using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog-api");

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "catalog-api",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Image = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    PublishAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrganizerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address_City = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Address_Complement = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Address_Country = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Address_District = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Address_Number = table.Column<int>(type: "integer", nullable: true),
                    Address_State = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Address_Street = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                schema: "catalog-api",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dates_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "catalog-api",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                schema: "catalog-api",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    NumberOfSeats = table.Column<int>(type: "integer", nullable: false),
                    DateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Dates_DateId",
                        column: x => x.DateId,
                        principalSchema: "catalog-api",
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dates_EventId",
                schema: "catalog-api",
                table: "Dates",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Name",
                schema: "catalog-api",
                table: "Events",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                schema: "catalog-api",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_DateId",
                schema: "catalog-api",
                table: "Sectors",
                column: "DateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sectors",
                schema: "catalog-api");

            migrationBuilder.DropTable(
                name: "Dates",
                schema: "catalog-api");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "catalog-api");
        }
    }
}
