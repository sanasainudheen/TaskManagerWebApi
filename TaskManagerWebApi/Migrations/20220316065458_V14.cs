using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[SP_ViewTaskDetails]  
                     @UserId varchar(75)   ,
					 @UserGroupTaskId int
                AS  
                BEGIN  
                   
					select TL.LogId, UGT.UserGroupTaskId,T.TaskName,TL.Attachment,TL.Note,T.TaskDescription,
					T.StartDate,T.EndDate,TL.CreatedOn
					from TaskLogTable TL
					inner join UserGroupTasks UGT on UGT.UserGroupTaskId=TL.UserGroupTaskId
					inner join Tasks T On T.TaskId=UGT.TaskId					
					where UserId=@UserId and UGT.StatusId in (2) and TL.UserGroupTaskId=@UserGroupTaskId
                END  ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
