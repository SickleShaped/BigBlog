using BigBlog.Models;
using BigBlog.Models.Db;

namespace BigBlog.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentById(Guid commentId);
        Task<List<Comment>> GetAllComments();
        Task AddComment(Comment comment, ClaimModel claimModel);
        Task EditComment(Guid commentId, Comment comment, ClaimModel claimModel);
        Task DeleteComment(Guid commentId, ClaimModel claimModel);
    }
}
