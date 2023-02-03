
using Microsoft.AspNetCore.Diagnostics;
using TypeAheadApi.Model;
using TypeAheadApi.Utils.Exceptions;
using System.Text.Json;

namespace TypeAheadApi
{
    public static class GlobalExceptionHandlerExtension
    {
        public static void UseCustomErrors(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<GlobalExceptionHandler>(loggerFactory);
        }
    }

    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public GlobalExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.next = next;
            logger = loggerFactory.CreateLogger<GlobalExceptionHandler>();
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception == null) return;

            ErrorModel error;

            logger.LogInformation($"Exception: {exception.GetType()}, {exception}");

            switch (exception)
            {
                case InvalidParameterException ex:
                    context.Response.StatusCode = 400;
                    error = new ErrorModel(ex);
                    break;
                case WordDoesNotExistException ex:
                    context.Response.StatusCode = 400;
                    error = new ErrorModel(ex);
                    break;
                default:
                    context.Response.StatusCode = 500;
                    error = new ErrorModel("Error while executing operation.");
                    logger.LogError(exception, "Projeto Padrão - Falha Inesperada.");
                    break;
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            // var domains = configuration.GetValue<string>("CorsConfiguration:Origin").Split(",");
            // context.Response.Headers.Add("Access-Control-Allow-Origin", domains);

            var json = JsonSerializer.Serialize(error, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, });
            await context.Response.WriteAsync(json);

            //find out how to use this
            // await next(context);
        }
    }
}
