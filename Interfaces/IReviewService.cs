using MoviesApiChallenge.Dtos;

namespace MoviesApiChallenge.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsOfOneMovieAsync(string movieId);
        Task AddReviewToMovieAsync(ReviewDto review);
        Task DeleteReviewAsync(string Id);
        Task UpdateReviewAsync(string Id, ReviewDto review);
    }
}
