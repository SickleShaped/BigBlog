using AutoMapper;
using BigBlog.Exceptions;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Role> GetRoleById(uint roleId)
        {
            var role = await _dbContext.Roles.Where(u => u.Id == roleId).FirstOrDefaultAsync();
            if(role == null) throw new ErrorException("GetRoleById: Роль не найдена!");
            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _dbContext.Roles.ToListAsync(); 
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
                if (alreadywas == true) { i++; alreadywas = false; } else { role.Id = i; break; }
            }

            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditRole(Role role, ClaimModel claimModel)
        {
            if (role.Id <= 3) throw new ErrorException("EditRole: Пользователь пытается изменить одну из основных ролей!");

            var dbRole = await GetRoleById(role.Id);
            if(dbRole != null && claimModel.RoleName == "Администратор")
            {
                dbRole.Name = role.Name;
                dbRole.Description = role.Description;
                await _dbContext.SaveChangesAsync();
            }
            else throw new ErrorException("EditRole: У пользователя недостатчно прав на это!");
        }

        public async Task DeleteRole(Role role, ClaimModel claimModel)
        {
            if (role.Id <= 3) throw new ErrorException("DeleteRole: Пользователь пытается удалить одну из основных ролей!");
            var dbRole = await GetRoleById(role.Id);
            if (dbRole != null && claimModel.RoleName == "Администратор")
            {
                _dbContext.Roles.Remove(dbRole);
                await _dbContext.SaveChangesAsync();
            }
            else throw new ErrorException("DeleteRole: У пользователя недостатчно прав на это!");
        }
    }
}
