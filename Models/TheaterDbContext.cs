using Microsoft.EntityFrameworkCore;

namespace MoviesApiChallenge.Models
{
    public class TheaterDbContext : DbContext
    {
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public TheaterDbContext()
        {

        }
        public TheaterDbContext(DbContextOptions<TheaterDbContext> options) : base(options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(mov =>
            {
                mov.HasOne<Reviews>(rev => rev.Reviews).WithOne( w => w.Movie).HasForeignKey<Reviews>(f => f.MovieId);
            });
        }
    }
}
