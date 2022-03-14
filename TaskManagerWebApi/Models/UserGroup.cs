using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }

        [Display(Name = "Group")]
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Groups { get; set; }
        [Display(Name = "ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public string IsActive { get; set; }
    }
}
