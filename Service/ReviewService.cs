using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Interfaces;
using MoviesApiChallenge.Models;

namespace MoviesApiChallenge.Service
{
    public class ReviewService : IReviewService
    {
        private readonly Serilog.ILogger logger;
        private readonly TheaterDbContext db;
        private IMapper mapper;

        public ReviewService(Serilog.ILogger logger, TheaterDbContext db, IMapper mapper)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddReviewToMovieAsync(ReviewDto review)
        {
            //var movie = await db.Movie.Where(w => w.Id == Guid.Parse(movieId)).FirstOrDefaultAsync();
            var reviewDtoToEntity = mapper.Map<Reviews>(review);
            var result = await db.Reviews.AddAsync(reviewDtoToEntity);
            await db.SaveChangesAsync();

            if (result.Entity.Id == Guid.Empty)
            {
                logger.Warning($"Something happened, The review was not saved");

            }
            logger.Information($"The added successfully");

        }

        public async Task DeleteReviewAsync(string Id)
        {
            var data = await db.Reviews.Where(w => w.Id == Guid.Parse(Id)).FirstOrDefaultAsync();

            if (data != null)
            {
                db.Reviews.Remove(data);
                db.SaveChanges();
                logger.Information($"The review was deleted");
            }

            logger.Warning("No movie was found");

        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsOfOneMovieAsync(string movieId)
        {
            var result = new List<ReviewDto>();

            if (db.Reviews == null)
            {
                logger.Information($"No movies were found");
                return result;

            }
            var review = await db.Reviews.Where(w => w.MovieId == Guid.Parse(movieId)).ToListAsync();
            logger.Information($"Returning collection of reviews, count: {review.Count()}");
            result = mapper.Map<IEnumerable<ReviewDto>>(review).ToList();
            return result;
        }

        public async Task UpdateReviewAsync(string Id, ReviewDto review)
        {
            var data = await db.Reviews.Where(w => w.Id == Guid.Parse(Id)).FirstOrDefaultAsync();

            if (data != null)
            {                
                data.Comment = review.Comment;
                logger.Information($"Saving the new entry");
                db.Entry(data).State = EntityState.Modified;
                await db.SaveChangesAsync();
                logger.Information($"The movie was updated successfully");
                
            }

            logger.Error($"The entry was not saved");
            
        }
    }
}
