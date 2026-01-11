using ITQTestApp.API.Contracts.Responses;
using ITQTestApp.Application.Exceptions;
using System.Text.Json;

namespace ITQTestApp.API.Middlewares
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Произошла необработанная ошибка. TraceId: {TraceId}",
                    context.TraceIdentifier);

                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var (statusCode, error) = exception switch
            {
                ValidationException validationException => (
                    StatusCodes.Status400BadRequest,
                    new ErrorResponse
                    {
                        Code = "validation_error",
                        Message = validationException.Message
                    }),

                JsonException jsonException => (
                    StatusCodes.Status400BadRequest,
                    new ErrorResponse
                    {
                        Code = "validation_error",
                        Message = jsonException.Message
                    }),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponse
                    {
                        Code = "internal_server_error",
                        Message = "Внутренняя ошибка сервера"
                    })
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(error);
        }
    }

}

