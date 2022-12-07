using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Interfaces;
using MoviesApiChallenge.Service;
using System.Net;

namespace MoviesApiChallenge.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private const string SUCCESS = "The request was process success";
        private const string FAILED = "The request was process success";
        public ReviewController(IReviewService reviewService, IMapper _mapper)
        {
            this.reviewService = reviewService;
        }

        // GET: api/Review
        [HttpGet("allreviews/{movieid}")]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetAsync(string movieId)
        {
            var operationID = Guid.NewGuid();

            try
            {
                var result = await reviewService.GetAllReviewsOfOneMovieAsync(movieId);
                var Response = new ResponseDTO(operationID, true, SUCCESS, result, HttpStatusCode.OK, "");
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, false, FAILED, "", HttpStatusCode.BadRequest, ex.Message));
            }

        }



        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> PostAsync(ReviewDto review)
        {
            var operationID = Guid.NewGuid();
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.OK, "Invalid Request, check your payload"));

            try
            {
                await reviewService.AddReviewToMovieAsync(review);
                var Response = new ResponseDTO(operationID, true, SUCCESS, "Review added successfully", HttpStatusCode.OK, "");
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.BadRequest, ex.Message));
            }
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> PutAsync([FromForm] string id, ReviewDto review)
        {
            var operationID = Guid.NewGuid();

            try
            {
                await reviewService.UpdateReviewAsync(id, review);
                var Response = new ResponseDTO(operationID, true, SUCCESS, "Review was updated successfully", HttpStatusCode.OK, "");
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO(operationID, true, FAILED, "", HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
