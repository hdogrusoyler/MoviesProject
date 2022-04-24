using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;

namespace MoviesProject.CrossCutting
{
    public class CustomCacheAttribute : ActionFilterAttribute, IActionFilter
    {
        public int Duration { get; set; }
        public ResponseCacheLocation Location { get; set; }
        public bool NoStore { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Duration != null)
            {
                context.HttpContext.Items["Duration"] = Duration;
            }

            if (Location != null)
            {
                context.HttpContext.Items["Location"] = Location;
            }

            if (NoStore)
            {
                context.HttpContext.Items["NoStore"] = NoStore;
            }

            base.OnActionExecuting(context);
        }

    }

    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string claimType, string claimValue) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public CustomAuthorizeFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            // Customize this object to fit your needs
            var result = new ObjectResult(new
            {
                context.Exception.Message, // Or a different generic message
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Log the exception
            _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

            // Set the result
            context.Result = result;
        }
    }
}