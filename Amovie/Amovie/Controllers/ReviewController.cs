using Behaviour.Interfaces;
using Entities.Models.ReviewDto;
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

        /// <summary>
        /// Add review
        /// </summary>
        /// <param name="newsDto"></param>
        /// <returns></returns>
        [HttpPost("addreview")]
        public async Task Add(AddReviewDto newsDto)
        {
            await _reviewService.AddReview(newsDto);
        }
        /// <summary>
        /// Delete Review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deletereview/{id}")]
        public async Task Delete(int id)
        {
            await _reviewService.DeleteReview(id);
        }

        /// <summary>
        /// Update review
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("updatereview/{id}")]
        public async Task Update(AddReviewDto reviewDto, int id)
        {
            await _reviewService.UpdateReview(reviewDto, id);
        }
    }
}
