using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharpDevMind.Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_RegisteredAt_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "registered_at",
                schema: "users",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registered_at",
                schema: "users",
                table: "users");
        }
    }
}
