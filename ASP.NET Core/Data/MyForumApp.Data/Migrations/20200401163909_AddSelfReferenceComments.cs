using Microsoft.EntityFrameworkCore.Migrations;

namespace MyForumApp.Data.Migrations
{
    public partial class AddSelfReferenceComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentParentId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentParentId",
                table: "Comments",
                column: "CommentParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentParentId",
                table: "Comments",
                column: "CommentParentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentParentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentParentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentParentId",
                table: "Comments");
        }
    }
}
