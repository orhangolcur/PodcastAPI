using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PodcastAPI.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var response = new
            {
                Title = "Bir hata oluştu",
                Errors = new List<string> { exception.Message }
            };

            if (exception is ValidationException validationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                response = new
                {
                    Title = "Doğrulama Hatası",
                    Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}