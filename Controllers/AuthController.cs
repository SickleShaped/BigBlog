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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string password, string email)
        {
            var user = await _authService.GetUserByEmailPassword(password, email);

            if (user == null) { return BadRequest(); }
            else
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };
                var identity = new ClaimsIdentity(claims, "MyAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return Redirect("/Home/ArticleAll");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            

            var claims = User.Claims.ToList();

            foreach (var claim in claims) {
                identity.RemoveClaim(claim);
                HttpContext.SignOutAsync();
            }



            return RedirectToAction("Login", "Home");
        }
    }
}
