using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class generatedata : Migration
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
                    MessageCode = table.Column<string>(nullable: true),
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
                    IsMarked = table.Column<bool>(nullable: false),
                    ResendOnId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
                    ReplyToId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSenders_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageSenders_Messages_ReplyToId",
                        column: x => x.ReplyToId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageSenders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageRecievers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeenDate = table.Column<DateTime>(nullable: true),
                    IsCc = table.Column<bool>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    IsMarked = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageRecievers_MessageSenders_MessageSenderId",
                        column: x => x.MessageSenderId,
                        principalTable: "MessageSenders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageRecievers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "DetailId", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "Mobile", "NationalCode", "PasswordHash", "PasswordSalt", "Phone", "PmPasswordHash", "PmUniqueId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 0, null, "نوید", false, "آیینه وند", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "09361060437", "98", new byte[] { 79, 140, 73, 202, 168, 137, 19, 1, 157, 97, 228, 16, 235, 70, 81, 129, 185, 200, 240, 15, 61, 12, 8, 248, 93, 28, 207, 161, 110, 189, 158, 74, 5, 94, 105, 90, 71, 11, 106, 153, 226, 247, 177, 14, 196, 9, 64, 101, 215, 160, 244, 135, 121, 64, 138, 43, 131, 77, 52, 22, 89, 123, 113, 130 }, new byte[] { 4, 252, 120, 40, 69, 115, 111, 231, 250, 182, 113, 99, 109, 99, 212, 242, 59, 116, 231, 239, 152, 166, 67, 199, 84, 58, 70, 105, 174, 160, 240, 139, 134, 126, 134, 122, 135, 222, 80, 191, 191, 16, 149, 81, 121, 84, 38, 249, 233, 57, 117, 12, 6, 244, 42, 118, 203, 191, 188, 47, 249, 202, 172, 112, 233, 110, 16, 79, 131, 214, 113, 87, 110, 54, 192, 128, 160, 88, 203, 201, 237, 245, 53, 206, 234, 182, 213, 69, 127, 122, 86, 192, 182, 235, 36, 61, 89, 14, 10, 40, 188, 225, 203, 69, 203, 90, 116, 166, 143, 138, 207, 90, 207, 151, 107, 255, 50, 114, 6, 180, 6, 68, 67, 238, 62, 39, 136, 191 }, null, null, null, "navid" });

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
                name: "IX_MessageSenders_ReplyToId",
                table: "MessageSenders",
                column: "ReplyToId",
                unique: true);

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
                name: "FK_MessageSenders_Messages_ReplyToId",
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
