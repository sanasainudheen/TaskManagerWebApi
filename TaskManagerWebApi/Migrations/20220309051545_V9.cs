using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[SP_GetUsersByGroupId]
    @GroupId int                
                AS
                BEGIN
                    SET NOCOUNT ON;
                  select Id,Name,UG.GroupId,UG.UserGroupId  from AspNetUsers U inner join UserGroups UG on UG.UserId=U.id
                  where UG.GroupId=@GroupId
                END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
