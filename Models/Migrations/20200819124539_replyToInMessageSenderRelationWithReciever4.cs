using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class replyToInMessageSenderRelationWithReciever4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                table: "MessageRecievers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 251, 146, 100, 49, 96, 32, 33, 123, 210, 144, 117, 243, 211, 230, 244, 154, 205, 242, 143, 107, 44, 220, 16, 189, 46, 67, 231, 57, 100, 205, 226, 153, 140, 16, 185, 199, 248, 167, 121, 129, 36, 38, 29, 141, 37, 129, 190, 144, 217, 81, 130, 83, 230, 231, 177, 67, 180, 29, 6, 231, 137, 1, 206, 103 }, new byte[] { 200, 170, 134, 129, 3, 205, 251, 177, 21, 216, 177, 32, 119, 125, 68, 132, 173, 232, 75, 69, 122, 222, 211, 53, 0, 124, 192, 251, 38, 133, 105, 195, 175, 139, 70, 94, 15, 195, 39, 100, 218, 24, 104, 16, 13, 162, 90, 76, 76, 48, 177, 74, 185, 205, 217, 81, 147, 213, 25, 221, 204, 154, 101, 48, 128, 207, 68, 116, 42, 247, 171, 23, 218, 107, 186, 29, 156, 247, 166, 13, 143, 181, 144, 33, 107, 96, 1, 3, 214, 107, 108, 159, 74, 155, 101, 105, 139, 206, 116, 205, 199, 131, 178, 14, 92, 126, 169, 180, 34, 112, 134, 123, 236, 148, 163, 217, 83, 234, 233, 157, 111, 169, 13, 82, 8, 50, 173, 220 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                table: "MessageRecievers",
                column: "MessageSenderId",
                principalTable: "MessageSenders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                table: "MessageRecievers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 198, 73, 148, 189, 3, 9, 226, 243, 209, 136, 32, 44, 180, 146, 187, 95, 63, 201, 163, 58, 81, 161, 50, 94, 14, 85, 103, 178, 242, 40, 22, 97, 246, 159, 152, 182, 122, 231, 108, 216, 239, 51, 180, 17, 34, 235, 10, 244, 6, 81, 4, 205, 13, 243, 57, 142, 5, 144, 118, 217, 120, 123, 25, 97 }, new byte[] { 67, 5, 117, 24, 193, 66, 34, 214, 62, 171, 128, 125, 109, 58, 240, 90, 147, 66, 5, 21, 181, 177, 119, 78, 221, 79, 134, 54, 143, 21, 56, 49, 123, 219, 83, 190, 254, 250, 124, 230, 57, 190, 66, 232, 102, 38, 64, 102, 34, 34, 169, 4, 175, 21, 74, 86, 137, 66, 226, 91, 9, 219, 70, 102, 66, 223, 50, 246, 124, 140, 217, 74, 55, 157, 167, 127, 147, 227, 145, 120, 7, 160, 81, 47, 102, 136, 144, 192, 206, 222, 105, 200, 110, 213, 15, 182, 57, 189, 128, 39, 29, 137, 176, 215, 101, 248, 213, 7, 107, 190, 139, 57, 218, 8, 26, 112, 160, 1, 155, 248, 200, 122, 104, 202, 36, 173, 247, 225 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                table: "MessageRecievers",
                column: "MessageSenderId",
                principalTable: "MessageSenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
