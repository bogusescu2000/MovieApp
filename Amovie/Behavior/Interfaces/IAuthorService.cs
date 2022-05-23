using Entities.Models.AuthorDto;

namespace Behaviour.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAll();
    }
}
