namespace RealEstate.API.Error
{
    public class ApiExceptionResponse : APIResponse
    {
        public ApiExceptionResponse(int statusCode, string? errorMessage = null, string? details = null) : base(statusCode, errorMessage)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
