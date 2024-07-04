namespace blackapi.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Timestamp = DateTime.Now;
            Message = message;
        }
    }
}
