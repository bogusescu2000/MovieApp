using Behaviour.Interfaces;
using Entities.Models.NewsDto;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns></returns>
        [HttpGet("allnews")]
        public async Task<ActionResult<List<NewsDto>>> GetAllNews()
        {
            var newsList = await _newsService.GetAll();
            return newsList;
        }

        /// <summary>
        /// Get last 3 news
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastnews")]
        public async Task<ActionResult<List<NewsDto>>> GetLast()
        {
            var newsList = await _newsService.GetLast();
            return Ok(newsList);
        }

        /// <summary>
        /// Get news by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("news/{id}")]
        public async Task<ActionResult<NewsDto>> GetNews(int id)
        {
            var news = await _newsService.GetNews(id);
            if (news == null)
                return NotFound("Such ID does not exists!");
            return Ok(news);
        }

        /// <summary>
        /// Add news
        /// </summary>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        [HttpPost("/addnews")]
        public async Task Add([FromForm] AddNewsDto newsDto)
        {
            await _newsService.AddNews(newsDto);
        }

        [HttpDelete("deletenews/{id}")]
        public async Task Delete(int id)
        {
            await _newsService.DeleteNews(id);
        }

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="newsDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task Update([FromForm] AddNewsDto newsDto, int id)
        {
            await _newsService.UpdateNews(newsDto, id);
        }

        /// <summary>
        /// Get paged news
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("/newspage/{page}")]
        public async Task<ActionResult<PagedNewsDto>> GetPagedNews(int page, int pageSize)
        {
            var pagedNews = await _newsService.GetPagedNews(page, pageSize);

            return Ok(pagedNews);
        }
    }
}
