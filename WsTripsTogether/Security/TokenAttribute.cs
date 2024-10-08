using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WsTripsTogether.Controllers;
using WsTripsTogether.Services.Login;
using WsTripsTogether.Utils;

namespace WsTripsTogether.Security;

public class TokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var loginHandler = context.HttpContext.RequestServices.GetService<ILoginHandler>() ?? throw new NullReferenceException();
        
        var auth = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(auth) || !auth.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var stringToken = auth.Split(" ")[1];

        // Check if it's present a logged user with same token
        var user = loginHandler.Users.SingleOrDefault(x => x.Token == stringToken);
        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        // Token validation
        var token = JwtUtils.DecodeToken(stringToken);
        if (!JwtUtils.IsValidToken(token, user.User))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (context.Controller is BaseController controller)
        {
            controller.UserLogged = user;
        }
        else
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        base.OnActionExecuting(context);
    }
}