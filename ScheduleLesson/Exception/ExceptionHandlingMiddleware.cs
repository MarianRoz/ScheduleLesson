namespace ScheduleLesson
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (StatusCodeException ex)
            {
                _logger.LogWarning("Користувач не знайдений");
                await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Внутрішня помилка сервера");
                await HandleExceptionAsync(context, 500, ex.Message);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new { Message = message };
            return context.Response.WriteAsync(message);
            //return context.Response.WriteAsync(new
            //{
            //    StatusCode = statusCode,
            //    Message = message,
            //    Context = context
            //}.ToString());
        }
    }
}