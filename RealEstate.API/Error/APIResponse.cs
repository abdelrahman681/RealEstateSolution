namespace RealEstate.API.Error
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetErrorMessageForStatusCode(StatusCode);
        }

        private string? GetErrorMessageForStatusCode(int statusCode)
        =>
            statusCode switch
            {
                500 => "Internal Server Error",
                400 => "Bad Request",
                404 => "Not Found",
                401 => "unauthorized",
                402 => "Payment Required",
                _ => "  "
            };


        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
