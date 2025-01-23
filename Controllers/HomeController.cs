using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Implementations;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, IUserService userService,IMapper mapper ,ICommentService commentService, IRoleService roleService, ITegService tegService)
        {
            _logger = logger;
            _articleService = articleService;
            _commentService = commentService;
            _roleService = roleService;
            _tegService = tegService;
            _userService = userService;
            _mapper = mapper;
        }



        [Authorize]
        public async Task<IActionResult> ArticleAdd()
        {
            var tegs = await _tegService.GetAllTegs();
            ViewBag.Tegs = tegs;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ArticleAll()
        {
            var articles = await _articleService.GetAllArticle();
            ViewBag.Articles = articles;
            return View();
        }

        [Authorize]
        [Route("Home/ArticleEdit%2F{id}")]
        public async Task<IActionResult> ArticleEdit(string id)
        {
            Guid articleId = Guid.Parse(id);
            var article = await _articleService.GetArticleById(articleId);
            var tegs = await _tegService.GetAllTegs();
            var art = _mapper.Map<AuxilaryArticle>(article);
            art.PossibleTegs = tegs;
            ViewBag.Article = art;

            return View();
        }

        [Authorize]
        [Route("Home/ArticlePage%2F{id}")]
        public async Task<IActionResult> ArticlePage(string id)
        {
            Guid articleId = Guid.Parse(id);
            var article = await _articleService.GetArticleById(articleId);
            ViewBag.Article = article;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CommentAll()
        {
            var comments = await _commentService.GetAllComments();
            ViewBag.Comments = comments;
            return View();
        }

        [Authorize]
        [Route("Home/CommentEdit%2F{id}")]
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

        public async Task<IActionResult> Login(string? returnUrl)
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
            var roles = await _roleService.GetAllRoles();
            ViewBag.Roles = roles;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> RoleAdd()
        {
            return View();
        }

        public async Task<IActionResult> SomethingWrong()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> RoleAll()
        {
            var roles = await _roleService.GetAllRoles();
            ViewBag.Roles = roles;
            return View();
        }

        [Authorize]
        [Route("Home/RoleEdit%2F{id}")]
        public async Task<IActionResult> RoleEdit(uint id)
        {
            var role = await _roleService.GetRoleById(id);
            ViewBag.Role = role;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> TegAdd()
        {
            return View();
        }

        [Authorize]
        [Route("Home/TegEdit%2F{id}")]
        public async Task<IActionResult> TegEdit(Guid id)
        {
            var teg = await _tegService.GetTegById(id);
            ViewBag.Teg = teg;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> TegAll()
        {
            var tegs = await _tegService.GetAllTegs();
            ViewBag.Tegs = tegs;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> UserAll()
        {
            var users = await _userService.GetAllUsers();
            ViewBag.Users = users;
            return View();
        }

        [Authorize]
        [Route("Home/UserEdit%2F{id}")]
        public async Task<IActionResult> UserEdit(Guid id)
        {
            var user = await _userService.GetUserById(id);

            var roles = await _roleService.GetAllRoles();
            var auxUser = _mapper.Map<AuxinaryUser>(user);
            auxUser.PossibleRoles = roles;
            ViewBag.User = auxUser;
            return View();
        }

        [Authorize]
        [Route("Home/UserPage%2F{id}")]
        public async Task<IActionResult> UserPage(Guid id)
        {
            var user = await _userService.GetUserById(id);
            ViewBag.User = user;
            return View();
        }
    }
}
