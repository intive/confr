using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intive.ConfR.Persistence.Migrations
{
    public partial class CommentsRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDateTime",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedDateTime",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CreatedDateTime",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedDateTime",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Comments");
        }
    }
}
