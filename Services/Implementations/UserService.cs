using AutoMapper;
using BigBlog.Exceptions;
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
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(User user, ClaimModel claimModel)
        {
            if (claimModel.Id == user.Id || claimModel.RoleName == "Администратор")
            {
                await _dbContext.Users.Where(u => u.Id == user.Id).ExecuteDeleteAsync();
            }
            else throw new ErrorException("DeleteUser: У пользователя недостатчно прав на это!");
        }

        public async Task EditUser(User user, ClaimModel claimModel)
        {
            if (claimModel.Id == user.Id || claimModel.RoleName == "Администратор")
            {
                var dbuser = await _dbContext.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
                if (dbuser != null)
                {
                    dbuser.FirstName = user.FirstName;
                    dbuser.LastName = user.LastName;
                    dbuser.Email = user.Email;
                    dbuser.Password = user.Password;
                    dbuser.RoleId = user.RoleId;
                    await _dbContext.SaveChangesAsync();
                }
                else throw new ErrorException("EditUser: Этот пользователь не найден!");
            }
            else throw new ErrorException("EditUser: У пользователя недостатчно прав на это!");
        }

        public async Task<List<User>> GetAllUsers()
        {
            var x = await _dbContext.Users.ToListAsync();
            return x;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var x = await _dbContext.Users.Include(x=>x.Role).Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (x != null) throw new ErrorException("GetUserById: Пользователь не найден!");
            return x;
        }
    }
}
