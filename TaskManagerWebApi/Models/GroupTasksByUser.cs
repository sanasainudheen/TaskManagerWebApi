using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models
{
    public class GroupTasksByUser
    {
        [Key]
        public int UserGroupTaskId { get; set; }
        public string GroupName { get; set; }            
        public string TaskName { get; set; }
        public string Status { get; set; }
    }
}
