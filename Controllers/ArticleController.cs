using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BigBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [HttpGet("GetArticleByUserId")]
        [Authorize]
        public async Task<Article> GetArticleByUserId(Guid id)
        {
            return await _articleService.GetArticleByUserId(id);
        }

        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<List<Article>> GetAllArticle()
        {
            return await _articleService.GetAllArticle();
        }

        [Authorize]
        [HttpPost("AddArticle")]
        public async Task<IActionResult> AddArticle(AuxilaryArticle articleToCreate)
        {
            Article article = _mapper.Map<Article>(articleToCreate);
            article.TegId = Guid.Parse(articleToCreate.TegCountNumber);

            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };
            
            await _articleService.AddArticle(article, claimModel);
            return Redirect("/Home/ArticleAll");
        }

        [Authorize]
        [HttpPost("EditArticle")]
        public async Task<IActionResult> EditArticle(AuxilaryArticle articleToCreate)
        {
            Article article = _mapper.Map<Article>(articleToCreate);
            article.TegId = Guid.Parse(articleToCreate.TegCountNumber);

            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _articleService.EditArticle(article, claimModel);
            return Redirect("/Home/ArticleAll");
        }

        [Authorize]
        [HttpPost("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(AuxilaryArticle articleToCreate)
        {
            Article article = _mapper.Map<Article>(articleToCreate);

            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _articleService.DeleteArticle(article, claimModel);
            return Redirect("/Home/ArticleAll");
        }


    }
}
