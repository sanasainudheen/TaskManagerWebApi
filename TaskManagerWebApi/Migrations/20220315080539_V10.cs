using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[SP_GetGroupTasksByUser]  
                      @UserId varchar(75)   
                AS  
                BEGIN  
                    SET NOCOUNT ON; 
                    select UGT.UserGroupTaskId,UGT.GroupId,UGT.TaskId,UGT.StatusId,  
					G.GroupName,T.TaskName,case UGT.statusid when 1 then 'Open'when 2 then 'Pending'  
					when 3 then 'InProgress'when 4 then 'Completed'END As Status  
					from UserGroupTasks UGT inner join Groups G on G.GroupId=UGT.GroupId  
					inner join Tasks T on T.TaskId=UGT.TaskId 					
					where UGT.GroupId in (select distinct groupid from UserGroups where UserId=@UserId)
					order by GroupId,StatusId
                END ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
