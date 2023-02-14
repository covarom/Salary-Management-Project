using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Contracts
{
    public class Response<T>
    {
        public T? Data { get; set; }

        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public Response(T? data, int statusCode, string? message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }

        public Response()
        {
        }
    }
}
