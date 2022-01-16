using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthModule.Model;
using AuthModule.Helpers;
using AuthModule.Extensions;
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
        private readonly UsersDbContext _context;

        public LoginController(UsersDbContext context)
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
            User? user = _context?.User?.FirstOrDefault(user => user.UserName == userName && user.Password == CreateHashString.GetHashString(password));
            if (user == null) {
                return new JsonResult(new { Succsess = false, Message = "User not found" } );
            }
            else {
                user.IsAuthenticated = true;
                HttpContext.Session.Set("User", user);
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
            User newUser = new()
            {
                IsAuthenticated = true,
                UserName = signUpUser.UserName,
                Password = CreateHashString.GetHashString(signUpUser.Password)
            };
            HttpContext.Session.Set("User", newUser);
            _context.User?.Add(newUser);
            _context.SaveChangesAsync();
            return new JsonResult(new { Succsess = true, UserId = newUser.Id });
        }
        [Authorize(Roles = "User")]
        [Route("Logout")]
        public void Logout() {
            HttpContext.Session.Set("User", new User());
        }
    }
}
