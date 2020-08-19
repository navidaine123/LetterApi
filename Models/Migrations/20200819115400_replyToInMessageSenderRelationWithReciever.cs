using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class replyToInMessageSenderRelationWithReciever : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 218, 118, 122, 165, 150, 3, 200, 58, 212, 176, 252, 104, 221, 201, 55, 175, 89, 240, 134, 122, 239, 183, 190, 174, 102, 25, 213, 254, 115, 122, 21, 194, 16, 126, 215, 174, 72, 64, 249, 208, 244, 23, 9, 54, 231, 30, 111, 118, 213, 190, 37, 151, 103, 12, 177, 221, 225, 140, 103, 172, 92, 25, 198, 65 }, new byte[] { 23, 137, 97, 235, 131, 137, 195, 247, 187, 209, 100, 185, 129, 249, 87, 134, 66, 194, 41, 93, 100, 202, 56, 78, 92, 119, 206, 171, 7, 212, 155, 166, 56, 52, 201, 73, 57, 188, 41, 27, 74, 116, 16, 192, 53, 243, 38, 187, 85, 63, 81, 47, 254, 31, 17, 216, 242, 63, 165, 78, 143, 189, 107, 162, 68, 164, 207, 111, 184, 226, 138, 66, 160, 192, 126, 42, 110, 50, 44, 188, 75, 116, 102, 149, 13, 169, 90, 51, 161, 121, 148, 232, 206, 142, 74, 206, 30, 157, 119, 45, 169, 150, 38, 132, 61, 10, 72, 101, 202, 154, 182, 145, 3, 166, 115, 9, 73, 18, 32, 122, 15, 21, 219, 252, 254, 215, 182, 61 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders",
                column: "ReplyToId",
                principalTable: "MessageRecievers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 198, 42, 27, 143, 170, 185, 226, 28, 191, 172, 186, 31, 74, 48, 167, 123, 199, 158, 103, 16, 25, 152, 75, 147, 108, 97, 13, 127, 154, 15, 159, 151, 122, 200, 81, 65, 109, 246, 42, 236, 67, 27, 35, 209, 50, 219, 137, 104, 48, 128, 213, 152, 188, 201, 48, 76, 102, 145, 19, 3, 8, 166, 19, 157 }, new byte[] { 70, 105, 58, 94, 204, 208, 85, 162, 166, 0, 76, 66, 41, 76, 114, 48, 190, 147, 133, 107, 65, 165, 171, 126, 37, 169, 91, 182, 185, 29, 225, 177, 33, 0, 225, 167, 18, 170, 163, 92, 149, 117, 77, 166, 243, 243, 242, 174, 83, 38, 65, 200, 36, 211, 177, 11, 136, 37, 58, 113, 99, 140, 1, 40, 65, 25, 185, 42, 131, 103, 126, 78, 122, 136, 60, 124, 183, 57, 149, 116, 88, 126, 82, 89, 226, 158, 23, 51, 81, 24, 63, 249, 135, 34, 231, 150, 151, 35, 24, 207, 243, 35, 110, 190, 237, 10, 193, 24, 151, 252, 97, 122, 141, 207, 177, 105, 148, 252, 221, 186, 206, 201, 8, 13, 227, 44, 148, 101 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ReplyToId",
                table: "MessageSenders",
                column: "ReplyToId",
                principalTable: "MessageRecievers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
