using FoodFolio.WebApi.Exceptions;

namespace FoodFolio.WebApi.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException nfe)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(nfe.Message);
            // Logowanie do logera
        }
        catch (BadRequestException bre)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(bre.Message);
            // Logowanie do logera
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);
            // Logowanie do logera
        }
    }
}

