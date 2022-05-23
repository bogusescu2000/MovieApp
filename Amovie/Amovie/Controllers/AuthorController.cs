using Behaviour.Interfaces;
using Entities.Models.AuthorDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        //Get all Authors
        [HttpGet("allauthors")]
        public async Task<ActionResult<List<AuthorDto>>> Get()
        {
            return Ok(await _authorService.GetAll());
        }
    }
}
