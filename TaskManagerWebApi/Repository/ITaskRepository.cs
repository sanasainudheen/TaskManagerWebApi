
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Repository
{
    public interface ITaskRepository
    {
        Task<int> CreateGroup(Group groupModel);
        Task<int> CreateTask(TaskModel TaskModel);

        Task<int> CreateUserGroup(UserGroup userGroup);

        Task<int> AssignTaskToUser(TaskLog taskLog);

        Task<int> CreateUserGroupTask(UserGroupTask userGroupTask);
        Task DeleteGroup(int groupId);

        Task<List<Group>> GetAllGroups();

        Task<List<FetchUserGroup>> GetAllUserGroups();

        Task<List<FetchUserGroupTask>> GetAllUserGroupTasks();
        Task<List<TaskModel>> GetAllTasks();
    }
}
