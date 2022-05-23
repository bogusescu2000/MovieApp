using AutoMapper;
using Behaviour.Interfaces;
using Entities.Entities;
using Entities.Models.ReviewDto;

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

        public async Task AddReview(AddReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                throw new ArgumentNullException(nameof(reviewDto));
            }
            else
            {
                var review = _mapper.Map<Review>(reviewDto);
                 await _repository.Add(review);
                 await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteReview(int id)
        {
            var reviews = await _repository.GetAll();
            var review = reviews.FirstOrDefault(m => m.Id == id);

            if (review == null)
            {
                throw new Exception("Review not found");
            }
            else
            {
                await _repository.Delete(review.Id);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task UpdateReview(AddReviewDto reviewDto, int id)
        {
            var review = await _repository.Get(id);

            if (review == null)
            {
                throw new Exception("Review not found!");
            }
            else
            {
                _mapper.Map(reviewDto, review);
                await _repository.Update(review);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
