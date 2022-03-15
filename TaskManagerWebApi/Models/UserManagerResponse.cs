using System.Collections.Generic;

namespace TaskManagerWebApi.Models
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public string RoleName { get; set; }

        public int ReturnValue { get; set; }
    }
}
