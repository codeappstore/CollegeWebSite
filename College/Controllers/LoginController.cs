using College.Access.IRepository;
using College.Helpers;
using College.Model.DataTransferObject.AuthDto;
using College.Model.DataTransferObject.AuthExtraDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace College.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthRepo _authRepo;

        public LoginController(IAuthRepo authRepo)
        {
            this._authRepo = authRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Check If is remembered session exists and is true
            var isRememberedToken = HttpContext.Session.GetString("_Remember");
            if (isRememberedToken != null && isRememberedToken.Replace(@"""", "") == "True")
            {
                // Check if user details session exists and redirect to specified controller
                var userDetails = HttpContext.Session.GetComplexData<AuthBasicDetailsModelDto>("_Details");
                if (userDetails != null)
                    return RedirectToAction("Index", "ControlPanel");
                HttpContext.Session.SetString("Warning", "Session Expired, Please Login First!");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModelDto login)
        {
            // Check If Model State is valid or not
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if user is valid login user
                    if (!await _authRepo.IsValidLogInAsyncTask(login))
                    {
                        HttpContext.Session.SetString("Warning", "Invalid email or password!");
                        return RedirectToAction(nameof(Index));
                    }
                    // Set session for remember token
                    if (login.RememberMe)
                        HttpContext.Session.SetString("_Remember", new string("True"));
                    // Fetch User credentials and store in session
                    var userDetails = await _authRepo.BasicUserDetailsAsyncTask(login.Email);
                    HttpContext.Session.SetComplexData("_Details", userDetails);
                    // Redirect to Control Panel with
                    HttpContext.Session.SetString("Success", "Welcome back " + userDetails.FullName);
                    return RedirectToAction("Index", "ControlPanel");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    HttpContext.Session.SetString("Error", "Problem connecting To server!");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Input fields might be empty or invalid!");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_Details");
            HttpContext.Session.Remove("_Remember");
            return RedirectToAction(nameof(Index));
        }
    }
}
