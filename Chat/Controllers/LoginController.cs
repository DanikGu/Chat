using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthModule.Helpers;
using AuthMiddlware.Extensions;
using AuthMiddlware.Helpers;
using System.Dynamic;
using Microsoft.Extensions.Logging;
using Chat.Models;

namespace Chat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly ChatDbContext _context;

        public LoginController(ChatDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn")]
        public JsonResult SignIn(LoginUser loginUser)
        {
            string userName = loginUser.UserName ?? "";
            string password = loginUser.Password ?? "";
            User? user = _context?.Users?.FirstOrDefault(user => user.UserName == userName && user.Password == CreateHashString.GetHashString(password));
            if (user == null) {
                return new JsonResult(new { Succsess = false, Message = "User not found" } );
            }
            else {
                user.IsAuthenticated = true;
                new LoginLogoutHelper().Login(user, HttpContext);
                //HttpContext.Session.Set("User", user);
                return new JsonResult(new { Succsess = true, UserId = user.Id });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("SignUp")]
        public JsonResult SignUp(LoginUser signUpUser) {
            if (signUpUser == null) {
                return new JsonResult(new { Succsess = false, Message = "User data are empty" });
            }
            if (_context.Users?.Any(user => user.UserName == signUpUser.UserName) ?? true) {
                return new JsonResult(new { Succsess = false, Message = "User with such username exist" });
            }
            User newUser = new()
            {
                IsAuthenticated = true,
                UserName = signUpUser.UserName,
                Password = CreateHashString.GetHashString(signUpUser.Password)
            };
            new LoginLogoutHelper().Login(newUser, HttpContext);
            //HttpContext.Session.Set("User", newUser);
            _context.Users?.Add(newUser);
            _context.SaveChangesAsync();
            return new JsonResult(new { Succsess = true, UserId = newUser.Id });
        }
        [Authorize(Roles = "User")]
        [Route("Logout")]
        public void Logout() {
            new LoginLogoutHelper().Logout(HttpContext);
            //HttpContext.Session.Set("User", new User());
        }
    }
}
