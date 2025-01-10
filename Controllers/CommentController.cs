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
        public async Task AddComment(Comment comment)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.AddComment(comment, claimModel);
        }

        [Authorize]
        [HttpPatch("EditComment")]
        public async Task EditComment(Guid id, Comment comment)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.EditComment(id, comment, claimModel);
        }

        [Authorize]
        [HttpDelete("DeleteComment")]
        public async Task DeleteComment(Guid id)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _commentService.DeleteComment(id, claimModel);
        }
    }
}
