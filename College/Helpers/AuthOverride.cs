using College.Access.IRepository;
using College.Model.DataTransferObject.AuthDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace College.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthOverride : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var detailsToken = context.HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
            var access = context.HttpContext.RequestServices.GetRequiredService<IAccessRepo>();
            if (detailsToken != null)
            {
                context.HttpContext.Session.SetComplexData("_Remember", new string("True"));
                var accessList = await access.FetchAccessByRoleIdFilter(detailsToken.RoleId);
                // Check if the page is can be accessed or not and redirect
                /* foreach (var accessModel in accessList)
                 {

                 }*/
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
