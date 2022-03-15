using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[SP_GetAssignedTasksByUser]  
                      @UserId varchar(75)   ,
					  @StatusId int
                AS  
                BEGIN  
                    SET NOCOUNT ON; 
                  		select TL.LogId, UGT.UserGroupTaskId,T.TaskName,TL.Attachment,TL.Note,T.TaskDescription,
					T.StartDate,T.EndDate,TL.CreatedOn
					from TaskLogTable TL
					inner join UserGroupTasks UGT on UGT.UserGroupTaskId=TL.UserGroupTaskId
					inner join Tasks T On T.TaskId=UGT.TaskId					
					where UserId=@UserId and UGT.StatusId=@StatusId
                END  ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTasks");
        }
    }
}
