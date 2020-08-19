using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class replyToInMessageSenderRelationWithReciever2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ReplyToId",
                table: "MessageSenders",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 103, 227, 196, 169, 231, 1, 69, 71, 105, 168, 95, 242, 181, 229, 124, 120, 102, 160, 198, 58, 162, 52, 130, 198, 210, 14, 111, 203, 12, 245, 23, 98, 253, 143, 39, 164, 246, 161, 142, 252, 184, 46, 75, 2, 46, 53, 21, 105, 11, 111, 25, 166, 146, 210, 42, 250, 5, 77, 165, 234, 148, 18, 229, 229 }, new byte[] { 70, 50, 170, 244, 173, 36, 172, 64, 228, 132, 122, 79, 171, 36, 32, 27, 97, 235, 197, 69, 98, 228, 57, 105, 197, 119, 240, 131, 4, 189, 239, 195, 201, 197, 22, 190, 208, 15, 89, 93, 243, 106, 213, 198, 94, 23, 144, 8, 253, 11, 248, 133, 114, 17, 131, 159, 93, 247, 129, 238, 184, 48, 128, 91, 112, 74, 145, 201, 209, 97, 131, 112, 243, 18, 147, 224, 234, 10, 50, 16, 136, 205, 225, 1, 64, 8, 219, 116, 113, 101, 51, 62, 48, 98, 127, 21, 106, 205, 61, 124, 134, 119, 31, 143, 186, 202, 101, 21, 237, 62, 240, 227, 206, 47, 67, 6, 208, 252, 234, 145, 48, 105, 97, 29, 81, 190, 53, 147 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ReplyToId",
                table: "MessageSenders",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 218, 118, 122, 165, 150, 3, 200, 58, 212, 176, 252, 104, 221, 201, 55, 175, 89, 240, 134, 122, 239, 183, 190, 174, 102, 25, 213, 254, 115, 122, 21, 194, 16, 126, 215, 174, 72, 64, 249, 208, 244, 23, 9, 54, 231, 30, 111, 118, 213, 190, 37, 151, 103, 12, 177, 221, 225, 140, 103, 172, 92, 25, 198, 65 }, new byte[] { 23, 137, 97, 235, 131, 137, 195, 247, 187, 209, 100, 185, 129, 249, 87, 134, 66, 194, 41, 93, 100, 202, 56, 78, 92, 119, 206, 171, 7, 212, 155, 166, 56, 52, 201, 73, 57, 188, 41, 27, 74, 116, 16, 192, 53, 243, 38, 187, 85, 63, 81, 47, 254, 31, 17, 216, 242, 63, 165, 78, 143, 189, 107, 162, 68, 164, 207, 111, 184, 226, 138, 66, 160, 192, 126, 42, 110, 50, 44, 188, 75, 116, 102, 149, 13, 169, 90, 51, 161, 121, 148, 232, 206, 142, 74, 206, 30, 157, 119, 45, 169, 150, 38, 132, 61, 10, 72, 101, 202, 154, 182, 145, 3, 166, 115, 9, 73, 18, 32, 122, 15, 21, 219, 252, 254, 215, 182, 61 } });
        }
    }
}
