using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApiChallenge.Models
{
    public class Reviews
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string description { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
