using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[SP_ViewTaskDetails]  
                        @UserGroupTaskId int,
                        @UserId varchar(75)   
					
                AS  
                BEGIN  
                   
					select TL.LogId, UGT.UserGroupTaskId,T.TaskName,TL.Attachment,TL.Note,T.TaskDescription,
					T.StartDate,T.EndDate,TL.CreatedOn,
					case UGT.StatusId  when 1 then 'Open' when 2 then 'Pending' when 3 then 'In Progress' 
					when 4 then 'Done'End As Status
					from TaskLogTable TL
					inner join UserGroupTasks UGT on UGT.UserGroupTaskId=TL.UserGroupTaskId
					inner join Tasks T On T.TaskId=UGT.TaskId					
					where UserId=@UserId and TL.StatusId in (2) and TL.UserGroupTaskId=@UserGroupTaskId
                END  ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
