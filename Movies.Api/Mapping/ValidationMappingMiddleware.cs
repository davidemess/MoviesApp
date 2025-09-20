using Movies.Application;
using Movies.Contracts.Responses;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Mapping
{
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidationMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationFailureResponse = new
                {
                    Errors = ex.Errors.Select(e => new ValidationResponse
                    {
                        PropertyName = e.PropertyName,
                        Message = e.ErrorMessage
                    })
                };
                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }
        }
    }
}
