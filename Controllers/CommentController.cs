using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BigBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpGet("GetCommentById")]
        public async Task<Comment> GetCommentById(Guid id)
        {
            return await _commentService.GetCommentById(id);
        }

        [Authorize]
        [HttpGet("GetAllComment")]
        public async Task<List<Comment>> GetAllComment()
        {
            return await _commentService.GetAllComments();
        }

        [Authorize]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.AddComment(comment, claimModel);
            return Redirect("/Home/ArticlePage%2F" + comment.ArticleId);
        }

        [Authorize]
        [HttpPost("EditComment")]
        public async Task<IActionResult> EditComment(Comment comment)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.EditComment(comment, claimModel);
            return Redirect("Home/ArticlePage%2F"+comment.ArticleId);
        }

        [Authorize]
        [HttpPost("DeleteComment")]
        public async Task<IActionResult> DeleteComment(Comment comment)
        {
            var dbcomment = await _commentService.GetCommentById(comment.Id);
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.DeleteComment(comment.Id, claimModel);
            return Redirect("Home/ArticlePage%2F" + comment.ArticleId);
        }
    }
}
