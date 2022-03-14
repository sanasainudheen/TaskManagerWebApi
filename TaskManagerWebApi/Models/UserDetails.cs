using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class UserDetails
    {
        [Key]
        public int UserGroupId { get; set; }
        public int GroupId { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
