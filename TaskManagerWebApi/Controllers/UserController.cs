using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWebApi.Context;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static string key = "b14ca5898a4e4142aace2ea2143a2410";
        private UserManager<ApplicationUser> userManager;
        private AppDbContext _context;
        public UserController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            this.userManager = userManager;
            this._context = context;
        }
       [HttpGet("list")]
       
        public IActionResult Index()
        {
            var users = userManager.Users;
            return Ok(users);
        }
      
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> getUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            var iser = await userManager.FindByIdAsync(id);
            if (iser == null)
                return NotFound();
            return Ok(iser);
        }
        [HttpGet("getUsersByGroupId/{id}")]
        public async Task<List<UserDetails>> getUsersByGroupId(string id)
        {
            //  SqlParameter groupId = new SqlParameter("@GroupId", id);
            List<SqlParameter> parms = new List<SqlParameter> {
            new SqlParameter { ParameterName = "@GroupId", Value = id }
    };
            var records =await _context.UserDetails.FromSqlRaw("EXEC SP_GetUsersByGroupId @GroupId", parms.ToArray()).ToListAsync();
            return records;          
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RegisterViewModel model)
        {               

            var user = new ApplicationUser
            {               
                Name = model.Name,
                Email = model.Email,
                UserName = model.UserName,
                IsBlock = model.IsBlock,
            };
            var result = await userManager.CreateAsync(user, EncryptString(model.Password));
            if (result.Succeeded)
            {
              //  await userManager.AddToRoleAsync(user, "User");

                return Ok(new UserManagerResponse
                {
                    Message = "User created successfully.",
                    IsSuccess = true,
                });
            }
            return Ok(new UserManagerResponse
            {
                Message = "User is not Created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update([FromBody] RegisterViewModel userModel, [FromRoute] string userId)
        {
            var iser = await userManager.FindByIdAsync(userId);
            if (iser == null)
            {
                return Ok(false);
            }
            iser.Email = userModel.Email;
            iser.Name = userModel.Name;
            iser.UserName = userModel.UserName;
            iser.PasswordHash = EncryptString(userModel.Password);
            var res = await userManager.UpdateAsync(iser);
            if (res.Succeeded)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "User details updated successfully.",
                    IsSuccess = true,
                });
            }
            return Ok(new UserManagerResponse
            {
                Message = "Some error occured",
                IsSuccess = false
            });
        }
        [HttpPut("blockuser/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        {
            var iser = await userManager.FindByIdAsync(userId);
            if (iser == null)
            {
                return Ok(false);
            }
           
            iser.IsBlock ="1";
            var res = await userManager.UpdateAsync(iser);
            if (res.Succeeded)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "User is blocked successfully.",
                    IsSuccess = true,
                });

            }
            return Ok(new UserManagerResponse
            {
                Message = "Some error occured",
                IsSuccess = false
            });
        }
            [HttpDelete("{userid}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userInGroup = _context.UserGroups.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (userInGroup == null)
                {
                    var res = await userManager.DeleteAsync(user);
                    if (res.Succeeded)
                    {
                        return Ok(new UserManagerResponse
                        {
                            Message = "User is deleted successfully.",
                            IsSuccess = true,
                        });

                    }
                }
                else
                {
                    return Ok(new UserManagerResponse
                    {
                        Message = "Couldnt delete the user since he/she is assigned to a group",
                        IsSuccess = false,
                    });
                }
                }
            return NotFound();
        }
        [HttpGet("getTheUsers")]
        public IActionResult getTheUsers()
        {

            var y = userManager.Users.Select(x => new
            {
                x.Id,
                x.Name
            });

            return Ok(y);
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
        public static string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = System.Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);//I have already defined "Key" in the above EncryptString function
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
      
    }
}
