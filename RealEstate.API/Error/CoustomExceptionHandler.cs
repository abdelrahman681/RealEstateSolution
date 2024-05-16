using System.Net;

namespace RealEstate.API.Error
{
    public class CoustomExceptionHandler
    {
        //the CLR Inject Object From This Type
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<CoustomExceptionHandler> _logger;

        public CoustomExceptionHandler(RequestDelegate Next, ILogger<CoustomExceptionHandler> logger, IHostEnvironment environment)
        {
            _next = Next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //Edit Content Type= application/Json
                //Edit Naming convention to camelcase
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _environment.IsDevelopment() ? new ApiExceptionResponse
                            ((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) : new ApiExceptionResponse
                            ((int)HttpStatusCode.InternalServerError);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
