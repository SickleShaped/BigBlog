using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BigBlog.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Role> GetUserById(Guid roleId)
        {
           
        }
        public async Task<List<Role>> GetAllRoles()
        {

        }
        public async Task AddRole(Role role)
        {
            var roles = await _dbContext.Roles.ToListAsync();
            uint i = 4;
            while (true)
            {
                bool alreadywas = false;
                
                foreach (var rol in roles)
                {
                    if (rol.Id == i) { alreadywas = true; }
                }
                if (alreadywas== true) { i++; alreadywas = false; } else { role.Id = i; break; }
            }

            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }
        public async Task EditRole(Guid roleId, Role role, ClaimModel claimModel)
        {

        }
        public async Task DeleteRole(Guid roleId, ClaimModel claimModel)
        {
            
        }
    }
}
