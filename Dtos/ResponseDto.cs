using System.Net;

namespace MoviesApiChallenge.Dtos
{
    public class ResponseDTO
    {
        public Guid OperationId { get; set; }
        public bool OperationSuccess { get; set; }        
        public string Message { get; set; }
        public object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ExceptionError { get; set; }

        public ResponseDTO()
        {

        }
        public ResponseDTO(Guid operationId, bool operationSuccess, string message, object data, HttpStatusCode statusCode, string exceptionError)
        {
            OperationId = operationId;
            OperationSuccess = operationSuccess;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            ExceptionError = exceptionError;
        }
    }

}
