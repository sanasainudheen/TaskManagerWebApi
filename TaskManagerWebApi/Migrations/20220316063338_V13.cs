using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[SP_GetAssignedTasksByUser]  
                      @UserId varchar(75)   ,
					  @StatusId int
                AS  
                BEGIN  
                    SET NOCOUNT ON; 
                  		select UGT.UserGroupTaskId,T.TaskName,T.TaskDescription,
					T.StartDate,T.EndDate from UserGroupTasks UGT inner join Tasks T On T.TaskId=UGT.TaskId		
				inner join UserGroups UG on UG.GroupId=UGT.GroupId
					where UserId=@UserId and UGT.StatusId in (2,3)
                END  ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
