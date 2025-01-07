using BigBlog.Services.Implementations;
using BigBlog.Services.Interfaces;

namespace BigBlog.BuilderServices
{
    public static class DependencyInjectionBuilderService
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ITegService, TegService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();

            return services;
        }
    }

}