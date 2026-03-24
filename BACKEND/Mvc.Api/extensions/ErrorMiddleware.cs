using DbModel.demoDb;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Mvc.Api.extensions
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // TRUCO: Inyectamos el _demoContext directamente aquí
        public async Task Invoke(HttpContext context, _demoContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si explota algo en cualquier parte del código, cae aquí
                await HandleExceptionAsync(context, ex, dbContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, _demoContext dbContext)
        {
            // 1. Guardar el error en la base de datos silenciosamente
            try
            {
                var errorLog = new ErrorLog
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    DateCreated = DateTime.UtcNow
                };

                dbContext.ErrorLog.Add(errorLog);
                await dbContext.SaveChangesAsync();
            }
            catch { /* Si falla al guardar el error, evitamos que rompa más cosas */ }

            // 2. Devolver una respuesta limpia (Error 500) a Angular
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var result = new
            {
                message = "Ocurrió un error interno en el servidor. El equipo de soporte ha sido notificado."
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}