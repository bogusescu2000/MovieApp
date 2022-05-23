using Entities.Models.GenreDto;

namespace BLL.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAll();
    }
}
