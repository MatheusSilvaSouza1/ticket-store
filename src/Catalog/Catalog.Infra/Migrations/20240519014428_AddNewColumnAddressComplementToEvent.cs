using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnAddressComplementToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "ticket-store-api",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                schema: "ticket-store-api",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishAt",
                schema: "ticket-store-api",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Complement",
                schema: "ticket-store-api",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PublishAt",
                schema: "ticket-store-api",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                schema: "ticket-store-api",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
