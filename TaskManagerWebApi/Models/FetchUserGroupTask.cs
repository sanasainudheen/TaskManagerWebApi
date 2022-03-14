using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class FetchUserGroupTask
    {
        [Key]
        public int UserGroupTaskId { get; set; }
        public int GroupId { get; set; }
        public int TaskId { get; set; }
        public int StatusId { get; set; }
        public string GroupName { get; set; }
        public string TaskName { get; set; }

        public string Status { get; set; }
    }
}
