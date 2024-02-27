namespace DemoProject.Middleware;

public class MijnExceptionLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var msg = $"[{DateTime.Now.ToShortTimeString()}] {e.Message}{Environment.NewLine}{e.StackTrace}{Environment.NewLine}{Environment.NewLine}";
            File.AppendAllText(@"C:\Temp\errors.log", msg);
            throw;
        }
    }
}

public static class MijnExceptionLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseMijnExceptionLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MijnExceptionLoggingMiddleware>();
    }
}