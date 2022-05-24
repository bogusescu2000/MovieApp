using Behaviour.Interfaces;
using DataAccess.Data;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Behaviour.Repositories
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly string _secureKey;

        public UserService(DataContext context)
        {
            _context = context;
            _secureKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SecretKey"];
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            user.Id = await _context.SaveChangesAsync();
            return user;
        }

        public Task<string> Generate(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(nameof(User.Name),user.Name.ToString()),
                new Claim(nameof(User.UserRole), user.UserRole!.ToString())
            };
            var payLoad = new JwtPayload(user.Id.ToString(), null, claims, null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payLoad);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }
        /// <summary>
        /// verify the jwt token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public async Task<JwtSecurityToken> Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Generate a jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
    }
}
