using CollegeWebsite.DataAccess.Models.Emails.Services.IRepo;
using CollegeWebsite.DataAccess.Models.Miscellaneous.Dtos;
using CollegeWebsite.Override;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CollegeWebsite.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IEmailConfigRepo _emailConfig;
        public LoginController(ILogger<LoginController> logger, IEmailConfigRepo _emailConfig)
        {
            _logger = logger;
            this._emailConfig = _emailConfig;
        }

        public async Task<IActionResult> Index()
        {
            var isRememberedToken = HttpContext.Session.GetString("_Remember");
            if (isRememberedToken != null && isRememberedToken.Replace(@"""", "") == "True")
            {
                var tokensToken = HttpContext.Session.GetComplexData<AuthorizedResponseDtro>("_Tokens");
                if (tokensToken != null)
                {
                    var detailsToken = HttpContext.Session.GetComplexData<BasicDetailsDto>("_Details");
                    if (detailsToken != null)
                    {
                        // Check If token expiration time is greater than current time
                        if (tokensToken.TokenExpiration > DateTime.UtcNow)
                        {
                            return RedirectToAction("Index", "ControlPanel");
                        }
                        else
                        { //Refresh Token
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

                                    HttpContext.Session.SetComplexData("_Tokens", authResponse);
                                    return RedirectToAction("Index", "ControlPanel");
                                }
                                else
                                {
                                    HttpContext.Session.SetString("Error", "Problem Connecting to server, Please Try Logging in!");
                                    return View();
                                }
                            }
                            else
                            {
                                return RedirectToAction(nameof(Index));
                            }

                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("Warning", "Unauthorized, Please Login First!");
                        return View();
                    }

                }
                else
                {
                    HttpContext.Session.SetString("Warning", "Session Expired, Please Login First!");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginInputClientDto login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var configDictionary = _emailConfig.GetEmailConfiguration();
                        LoginRequestServerDto serverRequest = new LoginRequestServerDto
                        {
                            Email = login.Email,
                            Password = login.Password
                        };

                        if (login.RememberMe)
                        {
                            HttpContext.Session.SetString("_Remember", new string("True"));
                        }

                        var baseUrl = configDictionary.BaseUrl;

                        var authenticationUrl = baseUrl + "/api/AuthenticateAsync";

                        var jsonObject = JsonConvert.SerializeObject(serverRequest);
                        var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(authenticationUrl, content);
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // Need To Fetch tokens and store it on session
                                var jsonResult = await response.Content.ReadAsStringAsync();
                                var authResponse = JsonConvert.DeserializeObject<AuthorizedResponseDtro>(jsonResult);

                                HttpContext.Session.SetComplexData("_Tokens", authResponse);
                                // Need To Fetch User details and store it on session
                                var detailsUrl = baseUrl + "/api/FetchDetailAsync?email=" + serverRequest.Email;

                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.JwtToken);
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var basicDetails = await client.GetAsync(detailsUrl);

                                if (basicDetails.IsSuccessStatusCode)
                                {
                                    var basicDetailsResponse = await basicDetails.Content.ReadAsStringAsync();

                                    //Get Data and store on Session anr response success
                                    var basicResponseDetailData =
                                        JsonConvert.DeserializeObject<BasicDetailsDto>(basicDetailsResponse);

                                    HttpContext.Session.SetComplexData("_Details", basicResponseDetailData);

                                    //  Update Access details left
                                    /* var accessDetails = await GetAccessdetails(basicResponseDetailData.AuthId);
                                     if (accessDetails != null)
                                     {
                                         var requestUrl = baseUrl + "/api/AccessDetailAsync";
                                         var requestJsonObject = JsonConvert.SerializeObject(accessDetails);
                                         var requestContent = new StringContent(requestJsonObject, Encoding.UTF8, "application/json");
                                         var accesHttpClient = new HttpClient();
                                         accesHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.JwtToken);
                                         accesHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                         var accessResponse =
                                             await accesHttpClient.PostAsync(requestUrl, requestContent);
                                         if (accessResponse != null)
                                         {
                                             if (accessResponse.IsSuccessStatusCode)
                                             {
                                                 HttpContext.Session.SetString("Success", "Welcome " + basicResponseDetailData.FullName);
                                                 return RedirectToAction("Index", "ControlPanel");
                                             }
                                         }
                                     }*/

                                    HttpContext.Session.SetString("Success", "Welcome " + basicResponseDetailData.FullName);
                                    return RedirectToAction("Index", "ControlPanel");
                                }
                                else
                                {
                                    HttpContext.Session.SetString("Error", "Problem connecting to server, Please retry!");
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                            else
                            {
                                HttpContext.Session.SetString("Warning", "Unauthorized!");
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            HttpContext.Session.SetString("Error", "Problem connecting to server, Please retry!");
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch
                {
                    HttpContext.Session.SetString("Error", "Problem connecting to server");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty!");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_Tokens");
            HttpContext.Session.Remove("_Details");
            HttpContext.Session.Remove("_Remember");
            return RedirectToAction(nameof(Index));
        }

        public async Task<AccessDetailsDto> GetAccessdetails(string AuthId)
        {
            if (!string.IsNullOrWhiteSpace(AuthId))
            {
                AccessDetailsDto access = new AccessDetailsDto();

                var client = new HttpClient();
                var EmailConfig = _emailConfig.GetEmailConfiguration();
                var queryURl = EmailConfig.ApiAccessUrl + EmailConfig.ApiKey;

                var apiDetails = await client.GetAsync(queryURl);

                if (apiDetails.IsSuccessStatusCode)
                {
                    var apiResponse = await apiDetails.Content.ReadAsStringAsync();

                    //Get Data and store on Session anr response success
                    var AccessIp =
                        JsonConvert.DeserializeObject<AccessAPIRequestDto>(apiResponse);

                    // Determine the IP Address of the request
                    access.AuthId = AuthId;
                    access.IpAddress = AccessIp.ip;
                    access.ContinentCode = AccessIp.continent_code;
                    access.CountryName = AccessIp.country_name;
                    access.City = AccessIp.city;
                    access.ZipCode = AccessIp.zip;
                    access.Latitude = AccessIp.latitude;
                    access.Longitude = AccessIp.longitude;
                    access.AccessAgent = Request.Headers["User-Agent"];
                    return access;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
