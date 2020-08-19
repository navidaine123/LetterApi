using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class replyToInMessageSender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReplyToId",
                table: "MessageSenders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 198, 42, 27, 143, 170, 185, 226, 28, 191, 172, 186, 31, 74, 48, 167, 123, 199, 158, 103, 16, 25, 152, 75, 147, 108, 97, 13, 127, 154, 15, 159, 151, 122, 200, 81, 65, 109, 246, 42, 236, 67, 27, 35, 209, 50, 219, 137, 104, 48, 128, 213, 152, 188, 201, 48, 76, 102, 145, 19, 3, 8, 166, 19, 157 }, new byte[] { 70, 105, 58, 94, 204, 208, 85, 162, 166, 0, 76, 66, 41, 76, 114, 48, 190, 147, 133, 107, 65, 165, 171, 126, 37, 169, 91, 182, 185, 29, 225, 177, 33, 0, 225, 167, 18, 170, 163, 92, 149, 117, 77, 166, 243, 243, 242, 174, 83, 38, 65, 200, 36, 211, 177, 11, 136, 37, 58, 113, 99, 140, 1, 40, 65, 25, 185, 42, 131, 103, 126, 78, 122, 136, 60, 124, 183, 57, 149, 116, 88, 126, 82, 89, 226, 158, 23, 51, 81, 24, 63, 249, 135, 34, 231, 150, 151, 35, 24, 207, 243, 35, 110, 190, 237, 10, 193, 24, 151, 252, 97, 122, 141, 207, 177, 105, 148, 252, 221, 186, 206, 201, 8, 13, 227, 44, 148, 101 } });

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenders_ReplyToId",
                table: "MessageSenders",
                column: "ReplyToId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders",
                column: "ReplyToId",
                principalTable: "MessageRecievers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders");

            migrationBuilder.DropIndex(
                name: "IX_MessageSenders_ReplyToId",
                table: "MessageSenders");

            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "MessageSenders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 237, 164, 102, 64, 13, 103, 222, 102, 212, 29, 194, 153, 148, 121, 146, 72, 168, 201, 100, 68, 2, 54, 62, 94, 39, 57, 164, 103, 29, 194, 98, 241, 0, 217, 134, 193, 9, 100, 221, 195, 123, 46, 159, 10, 126, 96, 162, 33, 199, 131, 253, 191, 211, 75, 122, 238, 198, 233, 32, 129, 125, 30, 12, 222 }, new byte[] { 55, 95, 77, 66, 127, 54, 214, 53, 22, 202, 46, 72, 46, 117, 47, 0, 187, 184, 32, 12, 158, 255, 202, 123, 110, 18, 183, 21, 217, 44, 124, 90, 10, 52, 68, 146, 27, 126, 167, 125, 113, 210, 145, 211, 59, 218, 113, 197, 203, 134, 135, 213, 255, 191, 150, 81, 229, 65, 122, 79, 118, 21, 13, 23, 132, 245, 220, 84, 211, 45, 178, 125, 2, 239, 10, 100, 38, 98, 55, 95, 20, 210, 13, 223, 80, 130, 41, 177, 158, 46, 29, 34, 24, 126, 149, 200, 216, 215, 240, 137, 58, 218, 113, 26, 125, 165, 58, 20, 126, 14, 109, 177, 145, 24, 9, 145, 28, 210, 253, 135, 220, 204, 160, 105, 213, 90, 73, 14 } });
        }
    }
}
