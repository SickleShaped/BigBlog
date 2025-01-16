using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBlog.Services.Implementations
{
    public class CommentService:ICommentService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddComment(Comment comment, ClaimModel claimModel)
        {
            comment.Id = Guid.NewGuid();
            comment.UserId = claimModel.Id;

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteComment(Guid commentId, ClaimModel claimModel)
        {
            var comment = await GetCommentById(commentId);
            if (claimModel.Id == comment.UserId || claimModel.RoleName == "Администратор" || claimModel.RoleName == "Модератор")
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditComment(Comment comment, ClaimModel claimModel)
        {
            var dbComment = await GetCommentById(comment.Id);
            if (claimModel.Id == comment.UserId || claimModel.RoleName == "Администратор" || claimModel.RoleName == "Модератор")
            {
                if (dbComment != null)
                {
                    dbComment.Text = comment.Text;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var x = await _dbContext.Comments.ToListAsync();
            return x;
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            var x = await _dbContext.Comments.Where(x => x.Id == commentId).FirstOrDefaultAsync();
            return x;
        }
    }
}
