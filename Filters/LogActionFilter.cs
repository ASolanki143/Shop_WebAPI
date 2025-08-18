using Microsoft.AspNetCore.Mvc.Filters;
using MyWebApiApp.Services.Interfaces;

namespace MyWebApiApp.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogServices _logServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogActionFilter(ILogServices logServices, IHttpContextAccessor httpContextAccessor)
        {
            _logServices = logServices;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string? userId = httpContext?.Session.GetInt32("UserID")?.ToString();
            string? username = httpContext?.Session.GetString("UserName");

            var actionName = context.ActionDescriptor.DisplayName;

            _logServices.InsertLog("Action", $"{actionName} executed by {username}", userId);
        }

        public void OnActionExecuted(ActionExecutedContext context){}
    }
}