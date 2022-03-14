using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManagerWebApi.Models
{

    public class ApplicationUser : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int UserId { get; set; }

        public string Name { get; set; }
        //   public string LastName { get; set; }
        //   public string DateOfBirth { get; set; }
        //    public string Gender { get; set; }
        // public string EmailId { get; set; }
        //   public string UserName { get; set; }
        //  public string Password { get; set; }
        //   public string IsActive { get; set; }
        public string IsBlock { get; set; }

    }
}

