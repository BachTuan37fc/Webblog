using aznews.Utilities;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        var method = context.Request.Method;
        Console.WriteLine("o day");
        if ((method == "GET" || method == "POST") && path.StartsWith("/Admin") && !path.StartsWith("/Admin/Login"))
        {
            if (Functions._Role != "Admin")
            {
                context.Response.Redirect("/Admin/Login");
                return;
            }
        }

        await _next(context);
    }
}
