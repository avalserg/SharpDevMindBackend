﻿//<auto-generated/>
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SharpDevMind.Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "users",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "users",
                columns: table => new
                {
                    permission_code = table.Column<string>(type: "character varying(100)", nullable: false),
                    role_name = table.Column<string>(type: "character varying(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.permission_code, x.role_name });
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_code",
                        column: x => x.permission_code,
                        principalSchema: "users",
                        principalTable: "permissions",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_roles_role_name",
                        column: x => x.role_name,
                        principalSchema: "users",
                        principalTable: "roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "permissions",
                column: "code",
                values: new object[]
                {
                    "categories:add",
                    "categories:archive",
                    "categories:read",
                    "categories:update",
                    "posts:add",
                    "posts:archive",
                    "posts:read",
                    "posts:search",
                    "users:read",
                    "users:update"
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permissions",
                columns: new[] { "permission_code", "role_name" },
                values: new object[,]
                {
                    { "categories:add", "Administrator" },
                    { "categories:archive", "Administrator" },
                    { "categories:read", "Administrator" },
                    { "categories:read", "Guest" },
                    { "categories:read", "User" },
                    { "categories:update", "Administrator" },
                    { "posts:add", "Administrator" },
                    { "posts:add", "User" },
                    { "posts:archive", "Administrator" },
                    { "posts:archive", "User" },
                    { "posts:read", "Administrator" },
                    { "posts:read", "Guest" },
                    { "posts:read", "User" },
                    { "posts:search", "Administrator" },
                    { "posts:search", "Guest" },
                    { "posts:search", "User" },
                    { "users:read", "Administrator" },
                    { "users:read", "User" },
                    { "users:update", "Administrator" },
                    { "users:update", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_name",
                schema: "users",
                table: "role_permissions",
                column: "role_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "users");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "users");
        }
    }
}