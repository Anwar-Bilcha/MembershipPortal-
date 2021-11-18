using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipPortal.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    lasttName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberAge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    academicRank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    academicTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fieldofStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    interestArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hostInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refrencePersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    memberPhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberCertificateURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberCVURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_Members_refrencePersonId",
                        column: x => x.refrencePersonId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_refrencePersonId",
                table: "Members",
                column: "refrencePersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
