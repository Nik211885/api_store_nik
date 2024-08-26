using ApplicationCore.Exceptions;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Common.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = GetStatusCode(ex);
            var reponse = new
            {
                titile = "Error",
                status = statusCode,
                detail = ex.Message,
                errors = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(reponse));
        }
        private static int GetStatusCode(Exception ex)
        {
            switch (ex.GetType().Name)
            {
                case nameof(ValidationException):
                    return StatusCodes.Status422UnprocessableEntity;
                case nameof(ArgumentException):
                case nameof(ArgumentNullException):
                    return StatusCodes.Status400BadRequest;
                case nameof(NotFoundException):
                    return StatusCodes.Status404NotFound;
                case nameof(UnauthorizedAccessException):
                    return StatusCodes.Status401Unauthorized;
                case nameof(ForbiddenException):
                    return StatusCodes.Status403Forbidden;
                default:
                    return StatusCodes.Status500InternalServerError;
            };
        }
    }
}
