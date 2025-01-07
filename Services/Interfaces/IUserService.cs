using BigBlog.Models;
using BigBlog.Models.Db;

namespace BigBlog.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid userId);
        Task<List<User>> GetAllUsers();
        Task AddUser(User user);
        Task EditUser(Guid userId, User user, ClaimModel claimModel);
        Task DeleteUser(Guid userId, ClaimModel claimModel);
    }
}
