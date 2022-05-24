using AutoMapper;
using Behaviour.Interfaces;
using Entities.Entities;
using Entities.Models.ReviewDto;
using Resources;

namespace Behaviour.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;
        private readonly IMapper _mapper;
        public ReviewService(IRepository<Review> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a review
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task AddReview(AddReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                throw new ArgumentNullException(nameof(reviewDto));
            }
            var review = _mapper.Map<Review>(reviewDto);
            await _repository.Add(review);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a review
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteReview(int id)
        {
            var reviews = await _repository.GetAll();
            var review = reviews.FirstOrDefault(m => m.Id == id);

            if (review == null)
            {
                throw new Exception(Resource.ReviewNotFound);
            }
            await _repository.Delete(review.Id);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Update a review
        /// </summary>
        /// <param name="reviewDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateReview(AddReviewDto reviewDto, int id)
        {
            var review = await _repository.Get(id);

            if (review == null)
            {
                throw new Exception("Review not found!");
            }
            _mapper.Map(reviewDto, review);
            await _repository.Update(review);
            await _repository.SaveChangesAsync();
        }
    }
}
