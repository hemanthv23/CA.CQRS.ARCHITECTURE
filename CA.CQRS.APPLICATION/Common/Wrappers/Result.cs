using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Common.Wrappers
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static Result<T> Failure(string message, List<string>? errors = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
