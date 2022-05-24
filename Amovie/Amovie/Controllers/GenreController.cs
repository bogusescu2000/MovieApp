using BLL.Interfaces;
using Entities.Models.GenreDto;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <returns></returns>
        [HttpGet("allgenres")]
        public async Task<ActionResult<List<GenreDto>>> Get()
        {
            return Ok(await _genreService.GetAll());
        }
    }
}
