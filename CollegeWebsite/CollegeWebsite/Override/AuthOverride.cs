using CollegeWebsite.DataAccess.Models.Emails.Services.IRepo;
using CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CollegeWebsite.Override
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthOverride : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var tokensToken = context.HttpContext.Session.GetComplexData<AuthorizedResponseDtro>("_Tokens");
            var detailsToken = context.HttpContext.Session.GetComplexData<BasicDetailsDto>("_Details");
            var _emailConfig = context.HttpContext.RequestServices.GetRequiredService<IEmailConfigRepo>();
            if (tokensToken != null && detailsToken != null)
            {
                context.HttpContext.Session.SetComplexData("_Remember", new string("True"));
                // Check If token expiration time is greater than current time
                if (tokensToken.TokenExpiration < DateTime.UtcNow)
                {
                    //Refresh Token
                    var configDictionary = _emailConfig.GetEmailConfiguration();
                    var baseUrl = configDictionary.BaseUrl;
                    var refreshUrl = baseUrl + "/api/AuthenticateAsync/Refresh";
                    var client = new HttpClient();

                    var jsonObject = JsonConvert.SerializeObject(tokensToken);
                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(refreshUrl, content);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResult = await response.Content.ReadAsStringAsync();
                            var authResponse = JsonConvert.DeserializeObject<AuthorizedResponseDtro>(jsonResult);
                            context.HttpContext.Session.SetComplexData("_Tokens", authResponse);

                        }
                        else
                        {
                            context.HttpContext.Session.Remove("_Tokens");
                            context.HttpContext.Session.Remove("_Details");
                            context.HttpContext.Session.Remove("_Remember");

                            context.HttpContext.Session.SetString("Error",
                                "Problem Connecting to server, Please Try Logging in!");
                            context.Result = new RedirectResult("~/Login/Index");
                        }
                    }
                    else
                    {
                        context.HttpContext.Session.Remove("_Tokens");
                        context.HttpContext.Session.Remove("_Details");
                        context.HttpContext.Session.Remove("_Remember");
                        context.HttpContext.Session.SetString("Error",
                            "Problem Connecting to server, Please Try Logging in!");
                        context.Result = new RedirectResult("~/Login/Index");
                    }

                }

            }
            else
            {
                context.HttpContext.Session.Remove("_Tokens");
                context.HttpContext.Session.Remove("_Details");
                context.HttpContext.Session.Remove("_Remember");
                context.HttpContext.Session.SetString("Warning",
                    "Unauthorized access, Login First!");
                context.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}
