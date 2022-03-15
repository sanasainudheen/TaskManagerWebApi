using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Repository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TaskManagerWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        public LoginController(IJwtAuth jwtAuth, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _jwtAuth = jwtAuth;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody] UserCredential _userCredential)
        { 
           
            var user = await userManager.FindByNameAsync(_userCredential.UserName);
            if (user == null)
            {
                return Ok(new UserManagerResponse { Message = "Wrong Creadentials", IsSuccess = false });
            }
            var token = _jwtAuth.Authentication(_userCredential.UserName, _userCredential.Password);
            if (token == null)
                return Unauthorized();
            var rolename = "User";
            if (await userManager.IsInRoleAsync(user, "Admin"))
                rolename = "Admin";
            //  return Ok(token);

            return Ok(new LoginResponse
            {
                Message = token,
                IsSuccess = true,
                RoleName = rolename,
                UserId= user.Id
               
            });
        }
    }
}
