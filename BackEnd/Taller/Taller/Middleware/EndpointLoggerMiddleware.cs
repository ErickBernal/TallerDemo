using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection; // Necesario para obtener servicios dentro del middleware
using Taller.Data;
using Taller.Entities;

public class EndpointLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Crear un alcance de servicios para resolver el DataContext
        using (var scope = context.RequestServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<DataContext>();
            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            var queryString = request.QueryString;

            var message = $"Request: {method} {path}{queryString}";

            var logEntry = new Log
            {
                Description = message,
                Date = DateTime.UtcNow
            };

            //Guardar en Db.
            dbContext.Logs.Add(logEntry);
            await dbContext.SaveChangesAsync();
        }
        // Invoca el siguiente middleware en la pipeline
        await _next(context);
    }
}
