using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models
{
    public class PendingTasks
    {
        [Key]
        public int UserGroupTaskId { get; set; }
        public string TaskName { get; set; }    

        public string TaskDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
