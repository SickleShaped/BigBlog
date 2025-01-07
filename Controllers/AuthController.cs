using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BigBlog.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        public AuthController(UserManager<User> userMgr, IAuthService authService)
        {
            _authService = authService;
            _userManager = userMgr;
        }

        [HttpPost]
        public async Task Login(string password, string email)
        {
            var user = await _authService.GetUserByEmailPassword(password, email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var identity = new ClaimsIdentity(claims, "MyAuth");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            //Сделать ретурн редирект
        }
    }
}
