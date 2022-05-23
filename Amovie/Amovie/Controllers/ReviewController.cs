using Behaviour.Interfaces;
using Entities.Models.ReviewDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amovie.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        //////Add Review
        [HttpPost("addreview")]
        public async Task Add(AddReviewDto newsDto)
        {
            await _reviewService.AddReview(newsDto);
        }
        //////Delete Review
        [HttpDelete("deletereview/{id}")]
        public async Task Delete(int id)
        {
            await _reviewService.DeleteReview(id);
        }

        //////Update Review
        [HttpPut("updatereview/{id}")]
        public async Task Update(AddReviewDto reviewDto, int id)
        {
            await _reviewService.UpdateReview(reviewDto, id);
        }
    }
}
