using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_Users_UserId",
                table: "MessageRecievers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_Users_UserId",
                table: "MessageSenders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "MessageSenders",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "MessageRecievers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 159, 115, 249, 18, 50, 113, 197, 235, 250, 67, 22, 48, 112, 236, 198, 239, 3, 93, 7, 155, 235, 243, 200, 162, 192, 187, 127, 116, 57, 188, 86, 146, 72, 79, 25, 60, 252, 75, 132, 228, 20, 35, 175, 38, 157, 24, 53, 129, 36, 67, 159, 178, 100, 228, 109, 69, 242, 247, 15, 179, 3, 51, 121, 75 }, new byte[] { 240, 41, 171, 149, 228, 230, 144, 19, 88, 66, 185, 78, 229, 134, 224, 52, 214, 105, 65, 136, 86, 116, 201, 199, 183, 231, 213, 242, 108, 108, 226, 7, 163, 38, 241, 218, 143, 137, 125, 194, 214, 166, 43, 12, 107, 236, 108, 149, 202, 107, 56, 241, 39, 94, 187, 65, 163, 164, 122, 171, 74, 34, 180, 198, 181, 249, 84, 177, 18, 212, 159, 98, 218, 251, 138, 139, 86, 233, 238, 19, 200, 42, 33, 217, 217, 101, 96, 32, 106, 214, 114, 168, 112, 67, 91, 102, 136, 5, 32, 74, 187, 104, 88, 126, 163, 22, 6, 4, 164, 172, 64, 57, 49, 16, 67, 38, 1, 157, 87, 153, 79, 39, 65, 5, 176, 208, 247, 77 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecievers_Users_UserId",
                table: "MessageRecievers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_Users_UserId",
                table: "MessageSenders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_Users_UserId",
                table: "MessageRecievers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_Users_UserId",
                table: "MessageSenders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "MessageSenders",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "MessageRecievers",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 29, 233, 196, 82, 123, 124, 48, 194, 205, 231, 169, 125, 223, 213, 233, 16, 30, 190, 170, 97, 156, 143, 202, 227, 28, 177, 3, 55, 222, 127, 131, 28, 248, 14, 176, 96, 142, 106, 42, 69, 209, 193, 156, 4, 250, 125, 219, 226, 59, 197, 216, 166, 223, 25, 85, 125, 124, 38, 87, 192, 221, 33, 10, 241 }, new byte[] { 187, 113, 132, 104, 144, 230, 210, 165, 110, 196, 7, 216, 79, 33, 117, 160, 15, 118, 171, 60, 19, 95, 175, 243, 193, 234, 20, 46, 242, 86, 46, 46, 75, 86, 175, 165, 83, 236, 151, 113, 84, 151, 220, 58, 104, 135, 78, 10, 208, 42, 41, 217, 180, 185, 82, 3, 105, 216, 35, 193, 231, 70, 52, 105, 18, 93, 239, 29, 76, 2, 44, 54, 58, 206, 198, 44, 111, 171, 156, 180, 212, 96, 35, 180, 195, 13, 248, 24, 222, 171, 106, 128, 57, 53, 92, 87, 58, 173, 225, 144, 97, 231, 197, 15, 144, 16, 168, 59, 4, 37, 82, 226, 21, 202, 225, 95, 162, 45, 13, 211, 0, 126, 87, 254, 225, 59, 206, 203 } });

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecievers_Users_UserId",
                table: "MessageRecievers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_Users_UserId",
                table: "MessageSenders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
