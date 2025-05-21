namespace API.Helper
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ResponseAPI(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageForStatusCode(statusCode);
        }

        private string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
