using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupTasks_UserGroups_UserGroupId",
                table: "UserGroupTasks");

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "UserGroupTasks",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupTasks_UserGroupId",
                table: "UserGroupTasks",
                newName: "IX_UserGroupTasks_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupTasks_Groups_GroupId",
                table: "UserGroupTasks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupTasks_Groups_GroupId",
                table: "UserGroupTasks");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "UserGroupTasks",
                newName: "UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupTasks_GroupId",
                table: "UserGroupTasks",
                newName: "IX_UserGroupTasks_UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupTasks_UserGroups_UserGroupId",
                table: "UserGroupTasks",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
