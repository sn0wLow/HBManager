using HBManager.Shared.Model.DTO.User;

namespace HBManager.Client
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(UserRegisterDTO request);
        Task<ServiceResponse<string>> Login(UserLoginDTO request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDTO request);
        Task<bool> IsUserAuthenticated();
    }
}
