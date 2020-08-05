using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    NationalCode = table.Column<string>(maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    Phone = table.Column<string>(maxLength: 11, nullable: true),
                    DetailId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    LockoutEnd = table.Column<DateTime>(nullable: false),
                    PmUniqueId = table.Column<string>(maxLength: 36, nullable: true),
                    PmPasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreateOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    MessageNumber = table.Column<string>(nullable: true),
                    ImportanceLevel = table.Column<byte>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageSenders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Prove = table.Column<string>(nullable: true),
                    IsSent = table.Column<bool>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    ResendOnId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSenders_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSenders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageRecievers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeenDate = table.Column<DateTime>(nullable: true),
                    IsCc = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: true),
                    MessageSenderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecievers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageRecievers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                        column: x => x.MessageSenderId,
                        principalTable: "MessageSenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageRecievers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "DetailId", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "Mobile", "NationalCode", "PasswordHash", "PasswordSalt", "Phone", "PmPasswordHash", "PmUniqueId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 0, null, "نوید", false, "آیینه وند", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "09361060437", "98", new byte[] { 236, 133, 41, 92, 178, 133, 29, 219, 59, 223, 53, 153, 94, 60, 166, 187, 116, 16, 240, 97, 34, 148, 216, 123, 47, 229, 89, 206, 123, 35, 57, 232, 143, 30, 85, 225, 123, 99, 221, 240, 75, 49, 179, 219, 86, 44, 96, 31, 209, 161, 100, 112, 101, 162, 253, 115, 93, 101, 211, 50, 5, 167, 247, 57 }, new byte[] { 190, 195, 99, 40, 223, 77, 183, 180, 80, 83, 12, 67, 15, 150, 60, 16, 145, 44, 176, 95, 190, 21, 114, 42, 119, 227, 142, 175, 250, 103, 18, 231, 208, 207, 238, 185, 46, 57, 161, 149, 64, 54, 206, 174, 211, 104, 32, 212, 198, 245, 172, 84, 127, 113, 17, 144, 251, 219, 78, 225, 114, 129, 204, 254, 220, 168, 19, 213, 103, 6, 197, 226, 207, 19, 79, 65, 97, 106, 82, 210, 189, 182, 35, 248, 34, 227, 60, 198, 197, 43, 58, 94, 169, 167, 43, 119, 34, 157, 239, 228, 45, 129, 209, 142, 15, 12, 111, 209, 35, 14, 211, 50, 31, 234, 153, 115, 209, 72, 40, 71, 69, 23, 56, 136, 11, 43, 194, 143 }, null, null, null, "navid" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecievers_MessageId",
                table: "MessageRecievers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecievers_MessageSenderId",
                table: "MessageRecievers",
                column: "MessageSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecievers_UserId",
                table: "MessageRecievers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedById",
                table: "Messages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenders_MessageId",
                table: "MessageSenders",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenders_ResendOnId",
                table: "MessageSenders",
                column: "ResendOnId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenders_UserId",
                table: "MessageSenders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSenders_MessageRecievers_ResendOnId",
                table: "MessageSenders",
                column: "ResendOnId",
                principalTable: "MessageRecievers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_Messages_MessageId",
                table: "MessageRecievers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageSenders_Messages_MessageId",
                table: "MessageSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                table: "MessageRecievers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MessageSenders");

            migrationBuilder.DropTable(
                name: "MessageRecievers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
