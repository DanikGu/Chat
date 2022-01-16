using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthModule.Model;
using AuthModule.Helpers;
using AuthModule.Extensions;
using System.Dynamic;
using Microsoft.Extensions.Logging;

namespace Chat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly UsersDbContext _context;

        public LoginController(UsersDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn")]
        public JsonResult Login(LoginRecord loginUser)
        {
            string userName = loginUser?.UserName ?? "";
            string password = loginUser?.Password ?? "";
            User? user = _context?.User?.FirstOrDefault(user => user.UserName == userName && user.Password == CreateHashString.GetHashString(password));
            if (user == null) {
                return new JsonResult(new { Succsess = false, Message = "User not found" } );
            }
            else {
                user.IsAuthenticated = true;
                HttpContext.Session.Set("User", user);
               // Response.Redirect(loginUser.RedirectUrl);
                return new JsonResult(new { Succsess = true });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult SignUp(dynamic signUpUser, string redirectUrl = "/") {
            if (signUpUser == null) {
                return new JsonResult(new { Succsess = false, Message = "User data are empty" });
            }
            if (signUpUser.Password != signUpUser.SecondPassword) {
                return new JsonResult(new { Succsess = false, Message = "Password are not the same" });
            }
            User newUser = new User()
            {
                IsAuthenticated = true,
                UserName = signUpUser.UserName,
                Password = CreateHashString.GetHashString(signUpUser.Password)
            };
            HttpContext.Session.Set("User", newUser);
            _context.User?.Add(newUser);
            _context.SaveChangesAsync();
            Response.Redirect(redirectUrl);
            return new JsonResult(new { Succsess = true });
        }
        [Authorize(Roles = "User")]
        public void Logout(string redirectUrl = "/") {
            HttpContext.Session.Set("User", new User());
            Response.Redirect(redirectUrl);
        }
    }
    public record LoginRecord(string UserName, string Password, string RedirectUrl);
}
