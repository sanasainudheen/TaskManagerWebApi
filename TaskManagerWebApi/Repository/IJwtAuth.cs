namespace TaskManagerWebApi.Repository
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
