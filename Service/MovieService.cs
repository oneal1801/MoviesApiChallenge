using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Interfaces;
using MoviesApiChallenge.Models;
using System.Linq;

namespace MoviesApiChallenge.Service
{
    public class MovieService : IMovieService
    {
        private readonly Serilog.ILogger logger;
        private readonly TheaterDbContext db;
        private IMapper mapper;

        public MovieService(Serilog.ILogger _logger, TheaterDbContext _db, IMapper _mapper)
        {
            logger = _logger;
            db = _db;
            mapper = _mapper;
        }

        public async Task<MovieDto> GetMovieByIdAsync(string Id)
        {
            var movie = new MovieDto();

            if (!MovieExists(Id, null))
            {
                logger.Information($"No movie with the given Id: {Id} was found");
                return movie;

            }

            var result = await db.Movie.FindAsync(Guid.Parse(Id));

            if (result != null)
            {
                logger.Information($"Returning the movie {movie.Title}");
                var resultMovie = mapper.Map<MovieDto>(movie);
                movie = resultMovie;
            }

            return movie;
        }

        public async Task<MovieDto> GetMovieByTitleAsync(string title)
        {
            var movie = new MovieDto();

            if (!MovieExists(title))
            {
                logger.Information($"No movie with the given Title: {title} was found");
                return movie;

            }

            var result = await db.Movie.FindAsync(title);

            if (result != null)
            {
                logger.Information($"Returning the movie {movie.Title}");
                var resultMovie = mapper.Map<MovieDto>(movie);
                movie = resultMovie;
            }

            return movie;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var result = new List<MovieDto>();

            if (db.Movie == null)
            {
                logger.Information($"No movies were found");
                return result;

            }
            var movies = await db.Movie.ToListAsync();
            logger.Information($"Returning collection of movies, count: {movies.Count()}");
            result = mapper.Map<IEnumerable<MovieDto>>(movies).ToList();
            return result;
        }

        public async Task<int> PostMovieAsync(MovieDto movie)
        {
            if (MovieExists(movie.Title))
            {
                logger.Information($"The movie {movie.Title} is already on our records");
                throw new Exception($"The movie {movie.Title} is already on our records");
            }

            movie.Status = 1;
            var movieDtoToEntity = mapper.Map<Movie>(movie);
            var result = await db.Movie.AddAsync(movieDtoToEntity);
            await db.SaveChangesAsync();

            if (result.Entity.Id == Guid.Empty)
            {
                logger.Warning($"Something happened, The movie was not saved, returned 0");
                return 0;
            }
            logger.Information($"The movie {movie.Title} was saved success");
            return 1;


        }

        public async Task<int> PutMovieAsync(string Id, UpdateMovieDto movie)
        {
            var data = await db.Movie.Where(w => w.Id == Guid.Parse(Id)).FirstOrDefaultAsync();

            if (data != null)
            {
                logger.Information($"Movie found and processing the update data");
                data.Genre = movie.Genre;
                data.Description = movie.Description;
                data.ReleaseDate = movie.ReleaseDate;
                logger.Information($"Saving the new entry");
                db.Entry(data).State = EntityState.Modified;
                await db.SaveChangesAsync();
                logger.Information($"The movie was updated successfully");
                return 1;
            }

            logger.Error($"The entry was not saved");
            return 0;


        }

        public async Task ChangeMovieStatusAsync(string Id, int status)
        {
            if (!MovieExists(Id, null))
            {
                logger.Error($"The movie was not found");
            }
            var movieStatus = await db.Movie.Where(w => w.Id == Guid.Parse(Id)).FirstOrDefaultAsync();
            if (movieStatus != null)
            {
                movieStatus.status = status;
                db.Entry(movieStatus).State = EntityState.Modified;
                logger.Information("The status of the movie was modified successfully");
                await db.SaveChangesAsync();
            }
        }

        public bool MovieExists(string title)
        {
            logger.Information($"Validating if Movie {title} exits");
            var exists = (db.Movie?.Any(e => e.Title == title)).GetValueOrDefault();
            logger.Information($"Movie exits: {exists}");
            return exists;
        }

        public bool MovieExists(string Id, string? title)
        {
            logger.Information($"Validating if Movie {title} exits");
            var exists = (db.Movie?.Any(e => e.Id == Guid.Parse(Id) && e.Title == title)).GetValueOrDefault();
            logger.Information($"Movie exits: {exists}");
            return exists;
        }

    }
}
