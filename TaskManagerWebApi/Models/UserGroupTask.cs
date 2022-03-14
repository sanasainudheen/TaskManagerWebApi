using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class UserGroupTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupTaskId { get; set; }
       
        [Display(Name = "Group")]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Groups { get; set; }

        [Display(Name = "Task")]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public virtual TaskModel Tasks { get; set; }
        
        public int  StatusId { get; set; }
       
        public string IsActive { get; set; }
    }
}
