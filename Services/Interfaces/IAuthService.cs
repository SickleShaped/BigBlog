using BigBlog.Models.Db;

namespace BigBlog.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUserByEmailPassword(string email, string password);
    }
}
