using BigBlog.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BigBlog.Middleware
{
    public class MiddlewareBuilderService
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareBuilderService> _logger;

        public MiddlewareBuilderService(RequestDelegate next, ILogger<MiddlewareBuilderService> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            string logFilePath = "log.txt";

            // Create a new StreamWriter and append to the log file
            StreamWriter logWriter = new StreamWriter(logFilePath, true);

            // Write a log message to the file


            try
            {
                logWriter.WriteLine(DateTime.Now.ToString() + $" [Information]: Пользователь отпавил запрос к {context.Request.Path}");

                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                logWriter.WriteLine(DateTime.Now.ToString() + $" [Error]: Доступ запрещен! Сообщение :{ex.Message}");
                context.Response.Redirect("/Home/AccessDenied");
            }
            catch (ErrorException ex)
            {
                logWriter.WriteLine(DateTime.Now.ToString() + $" [Error]: Ошибка! Сообщение :{ex.Message}");
                context.Response.Redirect("/Home/Error");
            }
            catch (Exception ex)
            {
                logWriter.WriteLine(DateTime.Now.ToString() + $" [Error]: Что-то пошло не так! Сообщение :{ex.Message}");
                context.Response.Redirect("/Home/SomethingWrong");
            }
            finally {logWriter.Close(); }

        }


        private async Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            
        }
    }
}
