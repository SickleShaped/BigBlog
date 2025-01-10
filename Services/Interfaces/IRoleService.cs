using BigBlog.Models.Db;
using BigBlog.Models;

namespace BigBlog.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetUserById(Guid roleId);
        Task<List<Role>> GetAllRoles();
        Task AddRole(Role role);
        Task EditRole(Guid roleId, Role role, ClaimModel claimModel);
        Task DeleteRole(Guid roleId, ClaimModel claimModel);
    }
}
