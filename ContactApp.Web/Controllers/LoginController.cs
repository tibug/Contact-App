using ContactApp.DataAccess.IRepository;
using ContactApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthentication _authentication;
        public LoginController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public IActionResult Index()
        {
            //var userAuthenticate = Request.Cookies["userAuthenticate"];
            //if (!string.IsNullOrEmpty(userAuthenticate))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //ClaimsPrincipal claimuser = HttpContext.User;
            //if (claimuser.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("userAuthenticate");

            if (HttpContext.Request.Cookies["userCheck"] != null)
            {
                Response.Cookies.Delete("userCheck");

            }
            if (HttpContext.Request.Cookies["searchlimit"] != null)
            {

                Response.Cookies.Delete("searchlimit");
            }

            return RedirectToAction("Index", "Login");
        }


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { success = false, message = "Email and password are Required" });
            }
            var result = await _authentication.LoginUser(data);
            if (result.IsSuccess)
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier,result.data.Email),
                    new Claim(ClaimTypes.Role,result.data.isAdmin?"admin":"user"),
                    new Claim(ClaimTypes.Name,result.data.FirstName),
                    new Claim(ClaimTypes.Email,result.data.Email),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties authprop = new()
                {
                    AllowRefresh = true,
                    IsPersistent = data.IsRemember,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authprop);


                return Ok(new { success = true, message = result.Message });
            }

            return Ok(new { success = false, message = result.Message });

        }


        [HttpPost]
        public async Task<IActionResult> RegUser([FromBody] Users data)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { success = false, message = "Email and password are Required" });
            }
            var result = await _authentication.RegisterUser(data);
            if (result.IsSuccess)
            {
                return Ok(new { success = true, message = result.Message });
            }

            return Ok(new { success = false, message = result.Message });

        }


    }
}
