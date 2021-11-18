using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MembershipPortal.Migrations
{
    public partial class Initialcorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "academicRank", "academicTitle", "city", "memberAge", "fieldofStudy", "firstName", "gender", "hostInstitution", "interestArea", "lasttName", "memberCVURL", "memberCertificateURL", "memberPhotoURL", "middleName", "refrencePersonId", "speciality" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "MSc.", "Lecturer", "Adama", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer Science", "Anwar", "Male", null, null, "Hussein", null, null, null, "Bilcha", null, "Software Engineering" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "academicRank", "academicTitle", "city", "memberAge", "fieldofStudy", "firstName", "gender", "hostInstitution", "interestArea", "lasttName", "memberCVURL", "memberCertificateURL", "memberPhotoURL", "middleName", "refrencePersonId", "speciality" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991987"), "BSc.", "Engineer", "Adama", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Civil Engineering", "Nebilr", "Male", null, null, "Abawari", null, null, null, "Nuru", null, "Civil Engineering" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991987"));
        }
    }
}
