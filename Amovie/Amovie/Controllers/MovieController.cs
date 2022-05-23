using Behaviour.Interfaces;
using Entities.Models.MovieDto;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/movies/")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("allmovies")]
        public async Task<ActionResult<List<MoviesDto>>> Get()
        {
            try
            {
                var result = await _movieService.GetAll();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get last 6 movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastmovies")]
        public async Task<ActionResult<List<LastMovieDto>>> GetLast()
        {
            try
            {
                var movieList = await _movieService.GetLast();
                if (movieList != null)
                {
                    return Ok(movieList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleMovieDto>> GetMovie(int id)
        {
            try
            {
                var result = await _movieService.GetMovie(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        [HttpPost("/create")]
        public async Task<ActionResult> AddMovieWithGenre(AddMovieDto movieDto)
        {
            await _movieService.AddMovie(movieDto);
            return Ok();
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task Update(AddMovieDto movieDto, int id)
        {
            await _movieService.UpdateMovie(movieDto, id);
        }

        //Delete Movie
        [HttpDelete("{id}")]
        public async Task DeleteMovie(int id)
        {
            await _movieService.DeleteMovie(id);
        }

        /// <summary>
        /// Get movies with pagination and sorting & filtering option
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("/pagedmovies")]
        public async Task<ActionResult<PagedMovieDto>> GetMovies(int page, int pageSize, string? sort, string? title)
        {
            try
            {
                var pagedMovies = await _movieService.GetPagedMovies(page, pageSize, sort, title);
                if (pagedMovies != null)
                {
                    return Ok(pagedMovies);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
