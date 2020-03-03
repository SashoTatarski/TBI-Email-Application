using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EMS.Data.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DboApplication");

            migrationBuilder.DropTable(
                name: "DboAttachment");

            migrationBuilder.DropTable(
                name: "DboLog");

            migrationBuilder.DropTable(
                name: "DboEmail");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GmailMessageId = table.Column<string>(nullable: false),
                    Received = table.Column<DateTime>(nullable: false),
                    SenderEmail = table.Column<string>(nullable: false),
                    SenderName = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ToNewStatus = table.Column<DateTime>(nullable: true),
                    ToCurrentStatus = table.Column<DateTime>(nullable: true),
                    ToTerminalStatus = table.Column<DateTime>(nullable: true),
                    NumberOfAttachments = table.Column<int>(nullable: true),
                    SizeOfAttachmentsMb = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    EGN = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SizeMb = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    LastStatusChange = table.Column<DateTime>(nullable: false),
                    NewStatus = table.Column<int>(nullable: false),
                    OldStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmailId",
                table: "Applications",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EmailId",
                table: "Attachments",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_EmailId",
                table: "Logs",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.CreateTable(
                name: "DboEmail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    GmailMessageId = table.Column<string>(nullable: false),
                    NumberOfAttachments = table.Column<int>(nullable: true),
                    Received = table.Column<DateTime>(nullable: false),
                    SenderEmail = table.Column<string>(nullable: false),
                    SenderName = table.Column<string>(nullable: true),
                    SizeOfAttachmentsMb = table.Column<double>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    ToCurrentStatus = table.Column<DateTime>(nullable: true),
                    ToNewStatus = table.Column<DateTime>(nullable: true),
                    ToTerminalStatus = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DboEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DboApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EGN = table.Column<string>(maxLength: 10, nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DboApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DboApplication_DboEmail_EmailId",
                        column: x => x.EmailId,
                        principalTable: "DboEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DboApplication_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DboAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SizeMb = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DboAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DboAttachment_DboEmail_EmailId",
                        column: x => x.EmailId,
                        principalTable: "DboEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DboLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmailId = table.Column<Guid>(nullable: false),
                    LastStatusChange = table.Column<DateTime>(nullable: false),
                    NewStatus = table.Column<int>(nullable: false),
                    OldStatus = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DboLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DboLog_DboEmail_EmailId",
                        column: x => x.EmailId,
                        principalTable: "DboEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DboLog_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DboApplication_EmailId",
                table: "DboApplication",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DboApplication_UserId",
                table: "DboApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DboAttachment_EmailId",
                table: "DboAttachment",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_DboLog_EmailId",
                table: "DboLog",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_DboLog_UserId",
                table: "DboLog",
                column: "UserId");
        }
    }
}
