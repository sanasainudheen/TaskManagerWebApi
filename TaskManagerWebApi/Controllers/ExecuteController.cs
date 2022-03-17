using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecuteController : ControllerBase
    {
        private static string key = "b14ca5898a4e4142aace2ea2143a2410";
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public ExecuteController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> config()
        {
            var roles = roleManager.Roles;           
            IdentityRole role1 = new IdentityRole
            {
                Name = "Admin"
            };
            IdentityRole role2 = new IdentityRole
            {
                Name = "User"
            };
          await  roleManager.CreateAsync(role1);
            await roleManager.CreateAsync(role2);
            ApplicationUser user = new ApplicationUser()
            {
                Name = "Admin",
                UserName = "admin",
                Email = "admin@mail.com"

            };
            await userManager.CreateAsync(user, EncryptString("Admin@123"));
            await userManager.AddToRoleAsync(user, "Admin");
               return Ok();
        }


        public static string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
    }
}
