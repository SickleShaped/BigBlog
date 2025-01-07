using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBlog.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.RoleId = 1;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid userId, ClaimModel claimModel)
        {
            if (claimModel.Id == userId || claimModel.RoleName == "Администратор")
            {
                await _dbContext.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
            }
        }

        public async Task EditUser(Guid userId, User user, ClaimModel claimModel)
        {
            if (claimModel.Id == userId || claimModel.RoleName == "Администратор")
            {
                var dbuser = await _dbContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
                if (dbuser != null)
                {
                    dbuser.FirstName = user.FirstName;
                    dbuser.LastName = user.LastName;
                    dbuser.Email = user.Email;
                    dbuser.Password = user.Password;
                    dbuser.RoleId = user.RoleId;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var x = await _dbContext.Users.ToListAsync();
            return x;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var x = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return x;
        }
    }
}
