using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace fiap2022.api.ActionFilters
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers["x-api-key"].Count ==0 ||
                context.HttpContext.Request.Headers["x-api-key"].FirstOrDefault() != "t0k3n"
                )
            {

                context.Result = new UnauthorizedResult();
            }
        }
    }
}
