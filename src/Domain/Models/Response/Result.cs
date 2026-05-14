using Domain.Models.Error;
using FluentValidation.Results;

namespace Domain.Models.Response
{
    public class Result<T>
    {
        public Result(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public Result(bool success, IEnumerable<CustomValidationFailure> errors)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public IEnumerable<CustomValidationFailure> Errors { get; }


        public static Result<T> Ok(string responseMessage = null, T responseData = default)
        {
            return new Result<T>(success: true, message: responseMessage, data: responseData);
        }

        public static Result<T> Error(string responseMessage)
        {
            return new Result<T>(success: false, responseMessage, data: default);
        }

        public static Result<T> Error(bool success, string responseMessage, T responseData = default)
        {
            return new Result<T>(success: success, responseMessage, data: responseData);
        }
    }
}
