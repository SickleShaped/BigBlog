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
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IRoleService _roleService;
        private readonly ITegService _tegService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ICommentService commentService, IRoleService roleService, ITegService tegService)
        {
            _logger = logger;
            _articleService = articleService;
            _commentService = commentService;
            _roleService = roleService;
            _tegService = tegService;
        }


        public async Task<IActionResult> ArticleAdd()
        {
            return View();
        }

        public async Task<IActionResult> ArticleAll()
        {
            var articles = await _articleService.GetAllArticle();
            ViewBag.Articles = articles;
            return View();
        }

        [Route("Home/ArticleEdit/{id}")]
        public async Task<IActionResult> ArticleEdit(Guid id)
        {
            var article = await _articleService.GetArticleById(id);
            ViewBag.Article = article;
            return View();
        }

        [Route("Home/ArticlePage%{id}")]
        public async Task<IActionResult> ArticlePage(Guid id)
        {
            var article = await _articleService.GetArticleById(id);
            ViewBag.Article = article;
            return View();
        }

        public async Task<IActionResult> CommentAll()
        {
            var comments = await _commentService.GetAllComments();
            ViewBag.Comments = comments;
            return View();
        }

        [Route("Home/CommentEdit/{id}")]
        public async Task<IActionResult> CommentEdit(Guid id)
        {
            var comment = await _commentService.GetCommentById(id);
            ViewBag.Comment = comment;  
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
            var roles = await _roleService.GetAllRoles();
            ViewBag.Roles = roles;
            return View();
        }

        [Route("Home/RoleEdit/{id}")]
        public async Task<IActionResult> RoleEdit(uint id)
        {
            var role = await _roleService.GetRoleById(id);
            ViewBag.Role = role;
            return View();
        }

        public async Task<IActionResult> TegAdd()
        {
            return View();
        }

        [Route("Home/TegEdit/{id}")]
        public async Task<IActionResult> TegEdit(Guid id)
        {
            var teg = await _tegService.GetTegById(id);
            ViewBag.Teg = teg;
            return View();
        }

        public async Task<IActionResult> TegAll()
        {
            var tegs = await _tegService.GetAllTegs();
            ViewBag.Tegs = tegs;
            return View();
        }

        public async Task<IActionResult> UserAll()
        {
            var users = await _userService.GetAllUsers();
            ViewBag.Users = users;
            return View();
        }

        [Route("Home/UserEdit/{id}")]
        public async Task<IActionResult> UserEdit(Guid id)
        {
            var user = await _userService.GetUserById(id);
            ViewBag.User = user;
            return View();
        }

        [Route("Home/UserPage/{id}")]
        public async Task<IActionResult> UserPage(Guid id)
        {
            var user = await _userService.GetUserById(id);
            ViewBag.User = user;
            return View();
        }


    }
}
