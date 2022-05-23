using Entities.Models.ReviewDto;

namespace Behaviour.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(AddReviewDto review);
        Task UpdateReview(AddReviewDto review, int id);
        Task DeleteReview(int id);
    }
}
