using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManagerWebApi.Repository;

namespace TaskManagerWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;
        public LoginController(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
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
        public IActionResult Authentication([FromBody] UserCredential _userCredential)
        {
            var token = _jwtAuth.Authentication(_userCredential.UserName, _userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
