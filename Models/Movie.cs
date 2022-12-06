using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesApiChallenge.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Genre { get; set; }

        public string? Description { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int status { get; set; }

        public virtual Reviews Reviews { get; set; }

    }
}
