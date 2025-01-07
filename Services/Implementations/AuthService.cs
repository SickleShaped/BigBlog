﻿using AutoMapper;
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

        public async Task<User> GetUserByEmailPassword(string email, string password)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.Password == password).Include(x=>x.Role).FirstOrDefaultAsync();
        }

    }
}
