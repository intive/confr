using Microsoft.EntityFrameworkCore.Migrations;

namespace Intive.ConfR.Persistence.Migrations
{
    public partial class CommentsWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Rooms_RoomId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RoomId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.AddColumn<string>(
                name: "UserDisplayName",
                table: "Comments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDisplayName",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "CommentID");

            migrationBuilder.AddColumn<string>(
                name: "RoomId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<string>(nullable: false),
                    RoomEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RoomId",
                table: "Comments",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Rooms_RoomId",
                table: "Comments",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
