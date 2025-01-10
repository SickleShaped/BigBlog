using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Implementations;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BigBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> ArticleAdd()
        {
            return View();
        }

        public async Task<IActionResult> ArticleAll()
        {
            return View();
        }

        public async Task<IActionResult> ArticleEdit()
        {
            return View();
        }

        public async Task<IActionResult> ArticlePage()
        {
            return View();
        }

        public async Task<IActionResult> CommentAll()
        {
            return View();
        }

        public async Task<IActionResult> CommentEdit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login()
        {
            var x = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid claimId = Guid.Empty;
            if (x != null) { claimId = Guid.Parse(x); }
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (claimId == Guid.Empty || claimRole == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ArticleAll");
            }

        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> RoleAdd()
        {
            return View();
        }

        public async Task<IActionResult> RoleAll()
        {
            return View();
        }

        public async Task<IActionResult> RoleEdit()
        {
            return View();
        }

        public async Task<IActionResult> TegAdd()
        {
            return View();
        }

        public async Task<IActionResult> TegEdit()
        {
            return View();
        }

        public async Task<IActionResult> TegAll()
        {
            return View();
        }

        public async Task<IActionResult> UserAll()
        {
            return View();
        }

        public async Task<IActionResult> UserEdit()
        {
            return View();
        }

        public async Task<IActionResult> UserPage()
        {
            return View();
        }


    }
}
