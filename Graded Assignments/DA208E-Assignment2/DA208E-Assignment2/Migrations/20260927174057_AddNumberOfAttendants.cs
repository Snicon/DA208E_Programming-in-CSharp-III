using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA208E_Assignment2.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfAttendants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAttendants",
                table: "Guests",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAttendants",
                table: "Guests");
        }
    }
}
