using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Interfaces;
using MoviesApiChallenge.Models;
using System.Net;

namespace MoviesApiChallenge.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        private const string SUCCESS = "The request was process success";
        private const string FAILED = "The request was process success";
        public MoviesController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetAsync()
        {            
            var operationID = Guid.NewGuid();

            try
            {
                var result = await movieService.GetMoviesAsync();
                var Response = new ResponseDTO(operationID, true, SUCCESS, result, HttpStatusCode.OK, new Exception());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, false, FAILED, "", HttpStatusCode.BadRequest, ex));
            }

        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetMovie(string id)
        {
            var operationID = Guid.NewGuid();

            try
            {
                var result = await movieService.GetMovieByIdAsync(id);
                var Response = new ResponseDTO(operationID, true, SUCCESS, result, HttpStatusCode.OK, new Exception());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, false, FAILED, "", HttpStatusCode.BadRequest, ex));
            }
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> PostMovie([FromForm] MovieDto movie)
        {
            var operationID = Guid.NewGuid();
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.OK, new Exception("Invalid payload, check your form")));

            try
            {
                var result = await movieService.PostMovieAsync(movie);
                var Response = new ResponseDTO(operationID, true, SUCCESS, result, HttpStatusCode.OK, new Exception());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.OK, ex));
            }
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> PutMovie([FromForm] string id, UpdateMovieDto movie)
        {
            var operationID = Guid.NewGuid();

            try
            {
                var result = await movieService.PutMovieAsync(id,movie);
                var Response = new ResponseDTO(operationID, true, SUCCESS, result, HttpStatusCode.OK, new Exception());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.OK, ex));
            }
        }

        // UpdateStatus: api/Movies/5
        [HttpPut("updatestatus/{id}")]
        public async Task<ActionResult<ResponseDTO>> ChangeMovieStatusAsync(string id, int status)
        {
            var operationID = Guid.NewGuid();

            try
            {
                await movieService.ChangeMovieStatusAsync(id, status);
                var Response = new ResponseDTO(operationID, true, SUCCESS, "Status was updated", HttpStatusCode.OK, new Exception());
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.OK, ex));
            }
        }

    }
}
