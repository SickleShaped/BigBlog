using BigBlog.BuilderServices;
using BigBlog.Middleware;
using BigBlog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Reflection.Metadata;

namespace BigBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDependencyInjection();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AssemblyReference.Assembly);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        var uri = redirectContext.RedirectUri;
                        UriHelper.FromAbsolute(uri, out var scheme, out var host, out var path, out var query, out var fragment);
                        uri = UriHelper.BuildAbsolute(scheme, host, path);
                        redirectContext.Response.Redirect("/Home/Login");
                        return Task.CompletedTask;
                    }
                };
            });



            var dataSource = new NpgsqlDataSourceBuilder(connection)
                .EnableDynamicJson()
                .Build();

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "BigBlog", Version = "v1" });
            });

            builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connection));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BigBlog V1"));

            }

            app.UseMiddleware<MiddlewareBuilderService>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseDBInitialize();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}");

            app.Run();
        }
    }
}
