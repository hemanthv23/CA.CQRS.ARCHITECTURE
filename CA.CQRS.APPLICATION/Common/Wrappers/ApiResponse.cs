using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.Common.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Failure(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
