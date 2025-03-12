using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Result
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public Exception? Exception { get; set; }

        public static Result WithSuccess() => new Result()
        {
            IsSuccess = true
        };

        public static Result WithError() => new Result()
        {
            IsSuccess = false
        };

        public static Result WithError(string? message) => new Result()
        {
            IsSuccess = false,
            Message = message,
        };

        public static Result WithError(Exception? exception) => new Result()
        {
            IsSuccess = false,
            Message = exception?.Message,
            Exception = exception,
        };
    }

    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public Exception? Exception { get; set; }

        public T? Data { get; set; }

        public static Result<T> WithSuccess() => new Result<T>()
        {
            IsSuccess = true,
        };

        public static Result<T> WithSuccess(T? data) => new Result<T>()
        {
            IsSuccess = true,
            Data = data
        };

        public static Result<T> WithError() => new Result<T>()
        {
            IsSuccess = false
        };

        public static Result<T> WithError(string? message) => new Result<T>()
        {
            IsSuccess = false,
            Message = message
        };

        public static Result<T> WithError(Exception? exception) => new Result<T>()
        {
            IsSuccess = false,
            Message = exception?.Message,
            Exception = exception,
        };

        public static Result<T> WithError(T? data, string? message) => new Result<T>()
        {
            IsSuccess = false,
            Message = message,
            Data = data
        };
    }
}
