namespace SalaryManagement.Contracts.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public Response(T data, int statusCode, string message)
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
