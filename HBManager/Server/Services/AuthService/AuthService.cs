using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HBManager.Server
{
    public class AuthService : IAuthService
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<string>> Login(string name, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));

            if (user is null)
            {
                response.Success = false;
                response.Message = "Dieser Benutzername existiert nicht.";
            }
            else if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Das Passwort ist nicht korrekt. ";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Success = true;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            if (await UserExists(user.Name))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Ein Benutzer mit diesem Namen existiert bereits."
                };
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            user.PasswordHash = passwordHash;

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return new ServiceResponse<string>
            {
                Data = CreateToken(user),
                Success = true,
                Message = "Registrierung erfolgreich!"
            };
        }


        public async Task<bool> UserExists(string name)
        {
            if (await this.context.Users.AnyAsync(user => user.Name.ToLower().Equals(name.ToLower())))
            {
                return true;
            }

            return false;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
               claims: claims,
               expires: DateTime.Now.AddDays(5),
               signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userID, string newPassword)
        {

            var user = await this.context.Users.FindAsync(userID);
            if (user is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Benutzer nicht gefunden."
                };
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.PasswordHash = passwordHash;


            await this.context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Password erfolgreich geändert."
            };
        }

        public async Task<User?> GetUserByName(string name)
        {
            return await this.context.Users.FirstOrDefaultAsync(u => u.Name.ToLower().Equals(name.ToLower()));
        }

        public async Task<int> GetUserID()
        {
            var user = this.httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user is null)
            {
                throw new Exception("User not found via HttpContext");
            }

            int userID = int.Parse(user);
            var userDB = await this.context.Users.FirstAsync(x => x.ID == userID);

            if (userDB is null)
            {
                throw new Exception("User not found in DB");
            }

            return userID;
        }

        public string? GetUserName()
        {
            return this.httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name);
        }


    }
}
