using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Repository;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository TaskRepository)
        {
            _taskRepository = TaskRepository;
        }
    
    [HttpGet("GetAllGroups")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _taskRepository.GetAllGroups();
        return Ok(groups);

    }
    [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] Group groupModel)
        {

            var id = await _taskRepository.CreateGroup(groupModel);
            if (id != 0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Group is created successfully.",
                    IsSuccess = true,
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                });
            }


        }
        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel taskModel)
        {

            var id = await _taskRepository.CreateTask(taskModel);
            if (id != 0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Task has beeen created successfully.",
                    IsSuccess = true,
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                });
            }


        }
        [HttpPost("CreateUserGroup")]
        public async Task<IActionResult> CreateUserGroup([FromBody] UserGroup userGroupModel)
        {

            var id = await _taskRepository.CreateUserGroup(userGroupModel);
            if (id != 0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "User has been added to the group successfully.",
                    IsSuccess = true,
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                });
            }


        }
        [HttpPost("CreateUserGroupTask")]
        public async Task<IActionResult> CreateUserGroupTask([FromBody] UserGroupTask userGroupTaskModel)
        {

            var id = await _taskRepository.CreateUserGroupTask(userGroupTaskModel);
            if (id != 0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Task has been added to the group successfully.",
                    IsSuccess = true,
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                });
            }


        }


        [HttpPost("AssignTaskToUser")]
        public async Task<IActionResult> AssignTaskToUser([FromBody] TaskLog taskLog)
        {

            var id = await _taskRepository.AssignTaskToUser(taskLog);
            if (id >0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Task has been assigned to the user successfully.",
                    IsSuccess = true,
                    ReturnValue=id
                });
            }
            else if(id == -1)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Task is already assigned",
                    IsSuccess = false,
                    ReturnValue = -1
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                    ReturnValue = 0
                });
            }
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int groupId)
        {
            await _taskRepository.DeleteGroup(groupId);
            return Ok(new UserManagerResponse
                {
                    Message = "User details deleted successfully.",
                    IsSuccess = true,
                });
            }
        [HttpGet("GetAllUserGroups")]

        public async Task<IActionResult> GetAllUserGroups()
        {
            var userGroups = await _taskRepository.GetAllUserGroups();
            return Ok(userGroups);

        }
        [HttpGet("GetAllTasks")]

        public async Task<IActionResult> GetAllTasks()
        {
            var Tasks = await _taskRepository.GetAllTasks();
            return Ok(Tasks);

        }
        [HttpGet("GetAllUserGroupTasks")]

        public async Task<IActionResult> GetAllUserGroupTasks()
        {
            var userGroups = await _taskRepository.GetAllUserGroupTasks();
            return Ok(userGroups);

        }
        [HttpGet("GetGroupTasksByUser/{id}")]
        public async Task<List<GroupTasksByUser>> GetGroupTasksByUser(string id)
        {
            var GroupTasksByUser = await _taskRepository.GetGroupTasksByUser(id);
            return GroupTasksByUser;
        }

        [HttpGet("AssignedTasksByUser/{id}/{statusId}")]
        public async Task<List<PendingTasks>> AssignedTasksByUser(string id,int statusId)
        {
            var AssignedTasksByUser = await _taskRepository.AssignedTasksByUser(id,statusId);
            return AssignedTasksByUser;
        }

        [HttpGet("ViewTaskDetails/{userGroupTaskId}/{userId}")]
        public async Task<List<AssignedTasks>> ViewTaskDetails(int userGroupTaskId,string userId)
        {
            var AssignedTasksDetails= await _taskRepository.ViewTaskDetails(userGroupTaskId, userId);
            return AssignedTasksDetails;
        }
        [HttpPost("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] TaskLog taskLog)
        {

            var id = await _taskRepository.UpdateUserStatus(taskLog);
            if (id != 0)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "status has been updated  successfully.",
                    IsSuccess = true,
                    ReturnValue = id
                });
            }
            else
            {
                return Ok(new UserManagerResponse
                {
                    Message = "Some error happened",
                    IsSuccess = false,
                });
            }
        }


    }
}
