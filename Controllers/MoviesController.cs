using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApiChallenge.Models;

namespace MoviesApiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly TheaterDbContext db;
        public MoviesController(TheaterDbContext _db)
        {
            db = _db;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (db.Movie == null)
            {
                return NotFound();
            }
            return await db.Movie.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(string id)
        {
            if (db.Movie == null)
            {
                return NotFound();
            }
          
            var movie = await db.Movie.FindAsync(Guid.Parse(id));

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            db.Movie.Add(movie);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> AddReview(Reviews reviews)
        {
            db.Reviews.Add(reviews);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = reviews.MovieId }, reviews);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(string id, Movie movie)
        {
            if (Guid.Parse(id) != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            if (db.Movie == null)
            {
                return NotFound();
            }

            var movie = await db.Movie.FindAsync(Guid.Parse(id));
            if (movie == null)
            {
                return NotFound();
            }

            db.Movie.Remove(movie);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(string id)
        {
            return (db.Movie?.Any(e => e.Id == Guid.Parse(id))).GetValueOrDefault();
        }

    }
}
