using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Models;
using System.Collections;

namespace MoviesApiChallenge.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<MovieDto> GetMovieByIdAsync(string Id);
        Task<MovieDto> GetMovieByTitleAsync(string title);
        Task<int> PostMovieAsync(MovieDto movie);
        Task<int> PutMovieAsync(string Id, UpdateMovieDto movie);
        Task ChangeMovieStatusAsync(string Id, int status);
        bool MovieExists(string title);
        bool MovieExists(string Id, string? title);

    }
}
