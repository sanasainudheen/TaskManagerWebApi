using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string IsActive { get; set; }
    }
}
