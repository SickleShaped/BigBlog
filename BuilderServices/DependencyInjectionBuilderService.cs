using BigBlog.Services.Implementations;
using BigBlog.Services.Interfaces;

namespace BigBlog.BuilderServices
{
    public static class DependencyInjectionBuilderService
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITegService, TegService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }

}