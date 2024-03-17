namespace HBManager.Server
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<bool> UserExists(string name);
        Task<ServiceResponse<string>> Login(string name, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        Task<int> GetUserID();
        string? GetUserName();
        Task<User?> GetUserByName(string name);
    }
}
