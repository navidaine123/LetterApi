using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class IsMarkedAndMessageREcieverDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMarked",
                table: "MessageSenders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "MessageRecievers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMarked",
                table: "MessageRecievers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 237, 164, 102, 64, 13, 103, 222, 102, 212, 29, 194, 153, 148, 121, 146, 72, 168, 201, 100, 68, 2, 54, 62, 94, 39, 57, 164, 103, 29, 194, 98, 241, 0, 217, 134, 193, 9, 100, 221, 195, 123, 46, 159, 10, 126, 96, 162, 33, 199, 131, 253, 191, 211, 75, 122, 238, 198, 233, 32, 129, 125, 30, 12, 222 }, new byte[] { 55, 95, 77, 66, 127, 54, 214, 53, 22, 202, 46, 72, 46, 117, 47, 0, 187, 184, 32, 12, 158, 255, 202, 123, 110, 18, 183, 21, 217, 44, 124, 90, 10, 52, 68, 146, 27, 126, 167, 125, 113, 210, 145, 211, 59, 218, 113, 197, 203, 134, 135, 213, 255, 191, 150, 81, 229, 65, 122, 79, 118, 21, 13, 23, 132, 245, 220, 84, 211, 45, 178, 125, 2, 239, 10, 100, 38, 98, 55, 95, 20, 210, 13, 223, 80, 130, 41, 177, 158, 46, 29, 34, 24, 126, 149, 200, 216, 215, 240, 137, 58, 218, 113, 26, 125, 165, 58, 20, 126, 14, 109, 177, 145, 24, 9, 145, 28, 210, 253, 135, 220, 204, 160, 105, 213, 90, 73, 14 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarked",
                table: "MessageSenders");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "MessageRecievers");

            migrationBuilder.DropColumn(
                name: "IsMarked",
                table: "MessageRecievers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 223, 163, 221, 154, 171, 169, 106, 70, 195, 136, 24, 209, 5, 108, 4, 79, 147, 234, 113, 10, 31, 142, 215, 23, 255, 153, 42, 30, 62, 156, 38, 210, 198, 165, 211, 68, 70, 164, 239, 27, 138, 223, 23, 109, 129, 118, 45, 147, 80, 152, 94, 172, 41, 209, 33, 198, 46, 144, 21, 14, 72, 197, 235, 98 }, new byte[] { 69, 177, 73, 96, 43, 30, 54, 177, 68, 98, 180, 188, 38, 19, 48, 51, 198, 26, 155, 70, 253, 191, 181, 151, 148, 46, 189, 118, 80, 21, 189, 137, 198, 223, 120, 144, 48, 143, 166, 129, 192, 39, 146, 227, 61, 75, 23, 14, 187, 165, 136, 130, 2, 102, 187, 167, 113, 168, 57, 168, 3, 21, 196, 176, 129, 202, 100, 47, 18, 199, 220, 38, 67, 116, 240, 239, 229, 74, 100, 130, 119, 115, 170, 150, 4, 224, 234, 4, 252, 237, 236, 182, 119, 10, 146, 113, 193, 99, 155, 227, 15, 42, 110, 115, 96, 2, 225, 66, 196, 97, 220, 174, 24, 105, 244, 13, 146, 225, 249, 212, 23, 16, 190, 38, 174, 245, 23, 175 } });
        }
    }
}
