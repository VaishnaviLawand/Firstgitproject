using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebApplication2
{
    public class SettingsessionGlobally :ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var value = context.HttpContext.Session.GetString("LoginName");
            if (value == null) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","Accounts" },
                    {"Action","Login" }
                });
            }
            base.OnActionExecuted(context);
        }
    }
}
