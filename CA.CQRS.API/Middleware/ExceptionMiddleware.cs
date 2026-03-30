using CA.CQRS.APPLICATION.Common.Wrappers;
using CA.CQRS.APPLICATION.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

// ✅ ADD THESE
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CA.CQRS.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                NotFoundException ex => new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Result = ApiResponse<string>.Failure(ex.Message)
                },

                ValidationException ex => new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Result = ApiResponse<string>.Failure("Validation failed",
                        ex.Errors.Select(e => e.ErrorMessage).ToList())
                },

                // ✅ ADDED: SQL Server Exception
                SqlException ex => new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Result = ApiResponse<string>.Failure("Database error occurred")
                },

                // ✅ ADDED: Generic DB Exception
                DbException ex => new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Result = ApiResponse<string>.Failure("Database connection issue")
                },

                _ => new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Result = ApiResponse<string>.Failure("An unexpected error occurred.")
                }
            };

            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response.Result));
        }
    }
}