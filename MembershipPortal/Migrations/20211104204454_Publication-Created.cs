using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipPortal.Migrations
{
    public partial class PublicationCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    PublicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pubSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pubDOI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    publicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    coAuthors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pubwebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contributingMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.PublicationId);
                    table.ForeignKey(
                        name: "FK_Publications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_MemberId",
                table: "Publications",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
