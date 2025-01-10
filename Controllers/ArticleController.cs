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
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
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
        public async Task AddArticle(Article article)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };
            
            await _articleService.AddArticle(article, claimModel);
        }

        [Authorize]
        [HttpPatch("EditArticle")]
        public async Task EditArticle(Guid id, Article article)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _articleService.EditArticle(id, article, claimModel);
        }

        [Authorize]
        [HttpDelete("DeleteArticle")]
        public async Task DeleteArticle(Guid id)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _articleService.DeleteArticle(id, claimModel);
        }


    }
}
