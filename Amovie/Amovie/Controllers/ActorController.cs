using BLL.Interfaces;
using Entities.Models.ActorDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Get all Actors
        /// </summary>
        /// <returns></returns>
        [HttpGet("allactors")]
        public async Task<ActionResult<List<ActorDto>>> Get()
        {
            return Ok(await _actorService.GetAll());
        }
    }
}
