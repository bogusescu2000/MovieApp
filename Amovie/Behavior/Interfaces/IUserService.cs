using Entities.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Behaviour.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        Task<JwtSecurityToken> Verify(string jwt);
        Task<string> Generate(User user);
    }
}
