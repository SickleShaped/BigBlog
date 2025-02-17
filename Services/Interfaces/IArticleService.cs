﻿using BigBlog.Models;
using BigBlog.Models.Db;

namespace BigBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<Article> GetArticleById(Guid articleId);
        Task<Article> GetArticleByUserId(Guid userId);
        Task<List<Article>> GetAllArticle();
        Task AddArticle(Article article, ClaimModel claimModel);
        Task EditArticle(Article article, ClaimModel claimModel);
        Task DeleteArticle(Article article, ClaimModel claimModel);
    }
}
