namespace MyWebApiApp.Middlewares
{
    public class SessionAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public SessionAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Auth Middleware "+context.Request.Path);
            if (context.Request.Path.StartsWithSegments("/api/user/LoginUser"))
            {
                await _next(context);
                return;
            }
            Console.WriteLine("Request ---- ");
            Console.WriteLine(context.Session.Id);
            var UserID = context.Session.GetInt32("UserID");
            Console.WriteLine(UserID);
            Console.WriteLine(context.Request.Body.ToString());
            if (!UserID.HasValue)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("User Not Logged in");
                return;
            }

            await _next(context);
        }
    }
}