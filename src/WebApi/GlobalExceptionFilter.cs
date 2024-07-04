using System.Text.Json;
using Domain.Shared;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi;

public static class GlobalExceptionFilter
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(a => a.Run(async context =>
        {
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            if (exception != null)
            {
                logger.LogError(exception, "Unhandled exception.");

                var errorResponse = new ErrorResponse();
                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    case ArgumentException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        errorResponse.Title = "Bad Request";
                        errorResponse.Message = exception.Message;
                        break;
                    case InvalidOperationException or DomainException:
                        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        errorResponse.Title = "Unprocessable Entity";
                        errorResponse.Message = exception.Message;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        errorResponse.Title = "Teste 500 - Internal Server Error";
                        errorResponse.Message = exception.Message;
                        break;
                }

                var jsonResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonResponse);
            }
        }));
    }
}

public class ErrorResponse
{
    public string Title { get; set; }
    public string Message { get; set; }
}