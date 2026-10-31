using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; }

        protected Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success(string message = "Operation successful") => new(true, message);
        public static Result Failure(string message) => new(false, message);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(T? data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string message = "Operation successful") => new(data, true, message);
        public static Result<T> Failure(string message) => new(default, false, message);
    }
}
