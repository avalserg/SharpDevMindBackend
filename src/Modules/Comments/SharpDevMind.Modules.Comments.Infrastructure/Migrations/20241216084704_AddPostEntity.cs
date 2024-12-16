using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharpDevMind.Modules.Comments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPostEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                schema: "comments",
                columns: table => new
                {
                    post_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.post_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_comments_post_id",
                schema: "comments",
                table: "comments",
                column: "post_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comments_posts_post_id",
                schema: "comments",
                table: "comments",
                column: "post_id",
                principalSchema: "comments",
                principalTable: "posts",
                principalColumn: "post_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_posts_post_id",
                schema: "comments",
                table: "comments");

            migrationBuilder.DropTable(
                name: "posts",
                schema: "comments");

            migrationBuilder.DropIndex(
                name: "ix_comments_post_id",
                schema: "comments",
                table: "comments");
        }
    }
}
