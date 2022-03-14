using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FetchUserGroupTasks",
                columns: table => new
                {
                    UserGroupTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FetchUserGroupTasks", x => x.UserGroupTaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FetchUserGroupTasks");
        }
    }
}
