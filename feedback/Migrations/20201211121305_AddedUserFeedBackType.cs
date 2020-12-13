using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedBack.Migrations
{
    public partial class AddedUserFeedBackType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedbackType",
                table: "UserFeedbacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackType",
                table: "UserFeedbacks");
        }
    }
}
