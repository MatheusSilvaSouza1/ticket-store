using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class EventsCanHaveMultipleDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sectors_Events_EventId",
                schema: "ticket-store-api",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "DateRange_End",
                schema: "ticket-store-api",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DateRange_Start",
                schema: "ticket-store-api",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                schema: "ticket-store-api",
                table: "Sectors",
                newName: "DateId");

            migrationBuilder.RenameIndex(
                name: "IX_Sectors_EventId",
                schema: "ticket-store-api",
                table: "Sectors",
                newName: "IX_Sectors_DateId");

            migrationBuilder.CreateTable(
                name: "Dates",
                schema: "ticket-store-api",
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
                        principalSchema: "ticket-store-api",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dates_EventId",
                schema: "ticket-store-api",
                table: "Dates",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sectors_Dates_DateId",
                schema: "ticket-store-api",
                table: "Sectors",
                column: "DateId",
                principalSchema: "ticket-store-api",
                principalTable: "Dates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sectors_Dates_DateId",
                schema: "ticket-store-api",
                table: "Sectors");

            migrationBuilder.DropTable(
                name: "Dates",
                schema: "ticket-store-api");

            migrationBuilder.RenameColumn(
                name: "DateId",
                schema: "ticket-store-api",
                table: "Sectors",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Sectors_DateId",
                schema: "ticket-store-api",
                table: "Sectors",
                newName: "IX_Sectors_EventId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRange_End",
                schema: "ticket-store-api",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRange_Start",
                schema: "ticket-store-api",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Sectors_Events_EventId",
                schema: "ticket-store-api",
                table: "Sectors",
                column: "EventId",
                principalSchema: "ticket-store-api",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
