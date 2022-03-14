using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }       
        public string IsActive { get; set; }
    }
}
