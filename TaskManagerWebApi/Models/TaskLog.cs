using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class TaskLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Display(Name = "UserGroupTask")]
        public virtual int UserGroupTaskId { get; set; }

        [ForeignKey("UserGroupTaskId")]
        public virtual UserGroupTask UserGrouptasks { get; set; }

        [Display(Name = "ApplicationUser")]
        public  string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }
        [Display(Name ="Status")]
        public virtual int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        public string Attachment { get; set; }

        public string Note { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }



    }
}
