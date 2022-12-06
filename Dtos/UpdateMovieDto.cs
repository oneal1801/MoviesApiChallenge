namespace MoviesApiChallenge.Dtos
{
    public class UpdateMovieDto
    {
        public string? Genre { get; set; }

        public string? Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
