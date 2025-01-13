using BigBlog.Models.Db;
using BigBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BigBlog.Services.Interfaces;

namespace BigBlog.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet("GetRoleById")]
        public async Task<Role> GetRoleById(uint id)
        {
            return await _roleService.GetRoleById(id);
        }

        [Authorize]
        [HttpGet("GetAllRoles")]
        public async Task<List<Role>> GetAllRoles()
        {
            return await _roleService.GetAllRoles();
        }

        [Authorize]
        [HttpPost("AddRole")]
        public async Task AddRole(Role role)
        {
            await _roleService.AddRole(role);
        }

        [Authorize]
        [HttpPatch("EditRole")]
        public async Task EditRole(uint id, Role role)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _roleService.EditRole(id, role, claimModel);
        }

        [Authorize]
        [HttpDelete("DeleteRole")]
        public async Task DeleteRole(uint id)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _roleService.DeleteRole(id, claimModel);
        }
    }
}
