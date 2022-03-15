using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerWebApi.Migrations
{
    public partial class V12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[SP_ViewTaskDetails]  
                      @LogId int
                AS  
                BEGIN  
                   
					select TL.LogId, UGT.UserGroupTaskId,T.TaskName,TL.Attachment,TL.Note,T.TaskDescription,
					T.StartDate,T.EndDate,TL.CreatedOn
					from TaskLogTable TL
					inner join UserGroupTasks UGT on UGT.UserGroupTaskId=TL.UserGroupTaskId
					inner join Tasks T On T.TaskId=UGT.TaskId					
					where LogId=@LogId
                END  ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
