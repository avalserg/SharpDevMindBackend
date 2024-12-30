﻿// <auto-generated/>
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmailProperyFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                schema: "quizzes",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "quizzes",
                table: "users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}