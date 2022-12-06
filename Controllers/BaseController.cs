using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApiChallenge.Dtos;
using MoviesApiChallenge.Models;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoviesApiChallenge.Controllers
{
    
    public class BaseController : ControllerBase
    {
        protected ResponseDTO CustomResponse(ResponseDTO response)
        {
            var customResponse = new ResponseDTO
            {
                OperationId= response.OperationId,
                OperationSuccess= response.OperationSuccess,
                Message= response.Message,
                StatusCode = response.StatusCode,
                Data = response.Data,
                ExceptionError= response.ExceptionError,

            };

            return customResponse;
        }
    }
}
