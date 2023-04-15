using System.Net;

namespace Application.ViewModels.AppResult
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObject)
        {
            StatusCode = HttpStatusCode.OK;
            Succeeded = true;
            ResultObject = resultObject;
            Message = "Success";
        }
        public ApiSuccessResult()
        {
            StatusCode = HttpStatusCode.OK;
            Succeeded = true;
        }
    }
}