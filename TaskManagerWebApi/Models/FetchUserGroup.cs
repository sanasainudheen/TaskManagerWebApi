using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{
    public class FetchUserGroup
    {
        [Key]
        public int UserGroupId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }


    }
}
