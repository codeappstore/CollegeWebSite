using College.Model.DataTransferObject.AuthDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace College.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthOverride : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var detailsToken = context.HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            if (detailsToken != null)
            {
                context.HttpContext.Session.SetComplexData("_Remember", new string("True"));
                if (detailsToken.RoleId == 3)
                {
                }
            }
            else
            {
                context.HttpContext.Session.Remove("_Details");
                context.HttpContext.Session.Remove("_Remember");
                context.HttpContext.Session.SetString("Warning",
                    "Unauthorized access, Login First!");
                context.Result = new RedirectResult("~/Login/Index");
            }
        }

    }
}