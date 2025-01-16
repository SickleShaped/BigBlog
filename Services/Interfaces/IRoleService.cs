using BigBlog.Models.Db;
using BigBlog.Models;

namespace BigBlog.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(uint roleId);
        Task<List<Role>> GetAllRoles();
        Task AddRole(Role role);
        Task EditRole(Role role, ClaimModel claimModel);
        Task DeleteRole(Role role, ClaimModel claimModel);
    }
}
