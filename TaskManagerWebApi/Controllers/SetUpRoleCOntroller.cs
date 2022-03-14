using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerWebApi.Controllers
{

    public class SetUpRoleCOntroller : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public SetUpRoleCOntroller(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = roleManager.Roles;
            if (roles.Count() > 0)
            {
                return View();
            }
            IdentityRole role1 = new IdentityRole
            {
                Name = "Admin"
            };
            IdentityRole role2 = new IdentityRole
            {
                Name = "User"
            };
            await roleManager.CreateAsync(role1);
            await roleManager.CreateAsync(role2);
            ApplicationUser user = new ApplicationUser()
            {
                Name="Admin",
                Email = "admin@gmail.com",
                UserName="admin"

            };
            await userManager.CreateAsync(user, "Admin@123");
            await userManager.AddToRoleAsync(user, "Admin");
            return View();
        }
    }
}
