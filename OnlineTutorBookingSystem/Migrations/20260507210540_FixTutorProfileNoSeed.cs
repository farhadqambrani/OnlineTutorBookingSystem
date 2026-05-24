using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTutorBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixTutorProfileNoSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TutorProfiles",
                keyColumn: "TutorProfileId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TutorProfiles",
                keyColumn: "TutorProfileId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TutorProfiles",
                keyColumn: "TutorProfileId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 8, 1, 42, 16, 69, DateTimeKind.Local).AddTicks(5832));

            migrationBuilder.UpdateData(
                table: "TutorProfiles",
                keyColumn: "TutorProfileId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 5, 8, 1, 42, 16, 69, DateTimeKind.Local).AddTicks(6536));
        }
    }
}
