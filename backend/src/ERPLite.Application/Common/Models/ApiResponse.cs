using ERPLite.Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPLite.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public static ApiResponse<T> Ok(
        T data,
        string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = HttpStatusCodes.Ok,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Created(
        T data,
        string message = "Created successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = HttpStatusCodes.Created,
                Message = message,
                Data = data
            };
        }       
    }
}
