namespace TaskManagerWebApi.Models
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public string RoleName { get; set; }

        public bool IsSuccess { get; set; }
    }
}
