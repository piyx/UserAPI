using System.Diagnostics;
using System.Text;

namespace UserApi.Middlewares
{
    public class RequestTimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(RequestDelegate next, ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var requestMethod = request.Method;
            var requestPath = request.Path;
            var requestBody = await ReadRequestBody(request);

            var stopWatch = Stopwatch.StartNew();
            
            await _next(context);
            stopWatch.Stop();

            var response = context.Response;
            var responseCode = response.StatusCode;

            _logger.LogInformation(
                "HTTP {RequestMethod} {RequestPath} responded {ResponseCode} in {ProcessingTime} ms",
                requestMethod,
                requestPath,
                responseCode,
                stopWatch.ElapsedMilliseconds
            );

            if (requestMethod == "POST" || requestMethod == "PUT")
            {
                _logger.LogInformation(
                    "Request Body Content: {RequestBody}",
                    requestBody
                );
            }
        }

        public async Task<string> ReadRequestBody(HttpRequest request)
        {
            // Read request body, and set position back to 0
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            
            return requestBody;
        }

    }

    public static class RequestTimeLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTimeLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimeLoggingMiddleware>();
        }
    }
}