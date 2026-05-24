using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTutorBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixTutorProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "TutorProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "TutorProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperTutor",
                table: "TutorProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "TutorProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LessonsCount",
                table: "TutorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "TutorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "TutorProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StudentsCount",
                table: "TutorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "IsSuperTutor",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "LessonsCount",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "StudentsCount",
                table: "TutorProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}