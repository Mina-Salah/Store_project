namespace Store.API.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        // Constructor with statusCode and custom message
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        // Default messages for specific status codes
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Request succeeded.",
                201 => "Resource created successfully.",
                400 => "Bad request. Please check your input.",
                401 => "Unauthorized. Please check your credentials.",
                404 => "Resource not found.",
                500 => "An unexpected error occurred. Please try again later.",
                _ => "An unknown status code was received."
            };
        }
    }

}
