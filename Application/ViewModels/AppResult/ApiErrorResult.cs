using System.Net;

namespace Application.ViewModels.AppResult
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();

        public ApiErrorResult()
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
        }

        public ApiErrorResult(string message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            Message = message;
            ValidationErrors.Add(message);
        }

        public ApiErrorResult(List<string> validationErrors)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            ValidationErrors = validationErrors;
        }
    }
}