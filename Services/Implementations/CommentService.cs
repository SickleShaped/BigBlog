using AutoMapper;
using BigBlog.Exceptions;
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
            if (comment == null) throw new ErrorException("DeleteComment: Комментарий не найден!");
            if (claimModel.Id == comment.UserId || claimModel.RoleName == "Администратор" || claimModel.RoleName == "Модератор")
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
            }
            else throw new ErrorException("DeleteComment: У пользователя недостатчно прав на это!");
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
                else throw new ErrorException("EditComment: Комментарйи не найден!");
            }
            else throw new ErrorException("EditComment: У пользователя недостатчно прав на это!");
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var x = await _dbContext.Comments.ToListAsync();
            return x;
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            var x = await _dbContext.Comments.Where(x => x.Id == commentId).FirstOrDefaultAsync();
            if (x == null) throw new ErrorException("GetCommentById: Комментарий не найден!");
            return x;
        }
    }
}
