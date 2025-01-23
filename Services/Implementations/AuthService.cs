using AutoMapper;
using BigBlog.Exceptions;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BigBlog.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> GetUserByEmailPassword(string password, string email)
        {
            var x = await _dbContext.Users.Where(x => x.Email == email && x.Password == password).Include(x => x.Role).FirstOrDefaultAsync();
            
            if(x == null ) throw new ErrorException("GetUserByEmailPassword: Пользователь не найден!");
            return x;
        }

    }
}
