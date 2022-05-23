
namespace Entities.Models.MovieDto
{
    public class PagedMovieDto
    {
        public List<MoviesDto> Movies { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
