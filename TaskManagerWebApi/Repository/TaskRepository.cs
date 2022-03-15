using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerWebApi.Context;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Repository
{
    public class TaskRepository:ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Group>> GetAllGroups()
        {
            var records = await _context.Groups.Select(x => new Group()
            {
               GroupId = x.GroupId,
                GroupName = x.GroupName,
                IsActive = x.IsActive
            }).ToListAsync();
            return records;
        }
       
        public async Task<int> CreateGroup([FromBody] Group groupModel)
        {

            var group = new Group
            {

               GroupName=groupModel.GroupName,
               IsActive=groupModel.IsActive

            };
            _context.Groups.Add(group);
          var result=  await _context.SaveChangesAsync();
            if (result > 0)
            {
                return group.GroupId;
            }
            else
                return 0;
        }
       
        public async Task<int> CreateTask([FromBody] TaskModel taskModel)
        {

            var taskNew = new TaskModel
            {

                TaskName = taskModel.TaskName,
                TaskDescription = taskModel.TaskDescription,
                StartDate= taskModel.StartDate,
                EndDate= taskModel.EndDate,
                IsActive= taskModel.IsActive


            };
            _context.Tasks.Add(taskNew);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return taskNew.TaskId;
            }
            else
                return 0;
        }
        public async Task<int> CreateUserGroup([FromBody] UserGroup userGroupModel)
        {

            var userGroupNew = new UserGroup
            {

                GroupId = userGroupModel.GroupId,
                UserId = userGroupModel.UserId,
                IsActive = userGroupModel.IsActive 
            };
            _context.UserGroups.Add(userGroupNew);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return userGroupNew.UserGroupId;
            }
            else
                return 0;
        }
        public async Task<int> CreateUserGroupTask([FromBody] UserGroupTask userGroupTaskModel)
        {

            var userGroupTaskNew = new UserGroupTask
            {

                GroupId = userGroupTaskModel.GroupId,
                TaskId = userGroupTaskModel.TaskId,
                IsActive = userGroupTaskModel.IsActive,
                StatusId= userGroupTaskModel.StatusId
            };
            _context.UserGroupTasks.Add(userGroupTaskNew);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return userGroupTaskNew.UserGroupTaskId;
            }
            else
                return 0;
        }
        public async Task DeleteGroup(int groupId)
        {
            var group = new Group () { GroupId = groupId };
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

        }
        public async Task<List<FetchUserGroup>> GetAllUserGroups()
        {
            var records = await _context.FetchUserGroups.FromSqlRaw("SP_GetUserGroups").ToListAsync();
            return records;
        }
        public async Task<List<TaskModel>> GetAllTasks()
        {
            var records = await _context.Tasks.Select(x => new TaskModel()
            {
                TaskId = x.TaskId,
                TaskName = x.TaskName,
                TaskDescription = x.TaskDescription,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsActive = x.IsActive
            }).ToListAsync();
            return records;
        }
        public async Task<List<FetchUserGroupTask>> GetAllUserGroupTasks()
        {

            var records = await _context.FetchUserGroupTasks.FromSqlRaw("SP_GetUserGroupTasks").ToListAsync();
            return records;
        }         

        public async Task<int> UpdateUserGroupTaskStatus(int StatusId, int UserGroupTaskId)
        {
            var rec = await _context.UserGroupTasks.FindAsync(UserGroupTaskId);
            if (rec != null)
            {
                rec.StatusId = StatusId; 
                await _context.SaveChangesAsync();
            }
            return rec.UserGroupTaskId;
        }

        public async Task<int> AssignTaskToUser([FromBody] TaskLog taskLog)
        {

            var newTask = new TaskLog
            {

                UserGroupTaskId = taskLog.UserGroupTaskId,
                UserId = taskLog.UserId,
                StatusId = taskLog.StatusId,
                Attachment=taskLog.Attachment ,
                Note = taskLog.Note,
                CreatedBy = taskLog.CreatedBy,
                CreatedOn = taskLog.CreatedOn,
            };
            _context.TaskLogTable.Add(newTask);
            var result = await _context.SaveChangesAsync();
            var TaskLogId = newTask.LogId;
            if (result > 0)
            {

                int res = await UpdateUserGroupTaskStatus(taskLog.StatusId, taskLog.UserGroupTaskId);
                return TaskLogId;
            }
            else
                return 0;
        }
    
        public async Task<List<GroupTasksByUser>> GetGroupTasksByUser(string id)
        {
            List<SqlParameter> parms = new List<SqlParameter> {
            new SqlParameter { ParameterName = "@UserId", Value = id }
    };
            var records = await _context.GroupTasks.FromSqlRaw("EXEC SP_GetGroupTasksByUser @UserId", parms.ToArray()).ToListAsync();
            return records;
        }

        public async Task<List<AssignedTasks>>AssignedTasksByUser(string id,int statusId)
        {
            var parameters = new List<SqlParameter>();
           
                parameters.Add(new SqlParameter("@UserId", id));
            parameters.Add(new SqlParameter("@StatusId", statusId));
        
            var records = await _context.AssignedTasks.FromSqlRaw("EXEC SP_GetAssignedTasksByUser @UserId,@StatusId", parameters.ToArray()).ToListAsync();
            return records;
        }

        public async Task<List<AssignedTasks>> ViewTaskDetails(int logId)
        {
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@LogId", logId));

            var records = await _context.AssignedTasks.FromSqlRaw("EXEC SP_ViewTaskDetails @LogId", parameters.ToArray()).ToListAsync();
            return records;
        }
    }
}
