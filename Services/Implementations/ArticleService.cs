using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace BigBlog.Services.Implementations
{
    public class ArticleService: IArticleService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public ArticleService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        public async Task AddArticle(Article article, ClaimModel claimModel)
        {
            article.Id = Guid.NewGuid();
            article.UserId = claimModel.Id;
            if (article.TegId == Guid.Empty) { article.TegId = Guid.Parse("00000000-0000-0000-0000-000000000001"); }

            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteArticle(Article article, ClaimModel claimModel)
        {
            var dbArticle = await GetArticleById(article.Id);
            if (dbArticle != null && (claimModel.Id == dbArticle.UserId || claimModel.RoleName == "Администратор" || claimModel.RoleName == "Модератор"))
            {
                _dbContext.Articles.Remove(dbArticle);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditArticle(Article article, ClaimModel claimModel)
        {
            var dbArticle = await GetArticleById(article.Id);
            if (dbArticle != null && (claimModel.Id == dbArticle.UserId || claimModel.RoleName == "Администратор" || claimModel.RoleName == "Модератор"))
            {
                dbArticle.Title = article.Title;
                dbArticle.Content = article.Content;
                dbArticle.TegId = article.TegId;
                await _dbContext.SaveChangesAsync();
            }         
        }

        public async Task<Article> GetArticleById(Guid articleId)
        {
            return await _dbContext.Articles.Include(x=>x.Teg).Include(X=>X.Comments).Where(x => x.Id == articleId).FirstOrDefaultAsync();
        }
        public async Task<List<Article>> GetAllArticle()
        {
            var x = await _dbContext.Articles.Include(x => x.Teg).ToListAsync();
            return x;
        }

        public async Task<Article> GetArticleByUserId(Guid userId)
        {
            var x = await _dbContext.Articles.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return x;
        }


    }
}
