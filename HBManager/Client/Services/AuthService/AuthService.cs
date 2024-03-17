using HBManager.Shared.Model.DTO.User;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
namespace HBManager.Client
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient http;
        private readonly AuthenticationStateProvider authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            this.http = http;
            this.authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<string>> Register(UserRegisterDTO request)
        {
            var result = await http.PostAsJsonAsync("api/auth/register", request);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<string>>())!;
        }
        public async Task<ServiceResponse<string>> Login(UserLoginDTO request)
        {
            var result = await this.http.PostAsJsonAsync("api/auth/login", request);

            return (await result.Content.ReadFromJsonAsync<ServiceResponse<string>>())!;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDTO request)
        {
            var result = await http.PostAsJsonAsync("api/auth/change-password", request.Password);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>())!;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await this.authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
        }

    }
}
