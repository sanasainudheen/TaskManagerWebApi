using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models
{
    public class AssignedTasks
    {
        [Key]
        public int LogId { get; set; }
        public int UserGroupTaskId { get; set; }
        public string TaskName { get; set; }
        public string Attachment { get; set; }

        public string Note { get; set; }
        public string TaskDescription { get; set; }
        public string CreatedOn { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
