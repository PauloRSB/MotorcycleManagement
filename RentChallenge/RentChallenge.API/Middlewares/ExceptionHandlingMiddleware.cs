using FluentValidation;
using RentChallenge.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RentChallenge.API.Middelwares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException)
            {
                await HandleExceptionAsync(context, "Dados inválidos", HttpStatusCode.BadRequest);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (BadHttpRequestException)
            {
                await HandleExceptionAsync(context, "Request mal formada", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new { mensagem = message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
