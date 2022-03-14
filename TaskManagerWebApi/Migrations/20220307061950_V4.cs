using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[SP_GetUserGroups]
                    
                AS
                BEGIN
                    SET NOCOUNT ON;
                  select UG.UserGroupId, G.GroupName,U.Name,G.GroupId,U.Id as UserId from UserGroups UG inner join Groups G ON G.GroupId=UG.GroupId
                    inner join AspNetUsers U ON U.Id=UG.UserId where UG.IsActive=1
                END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
