using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BigBlog.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<User> GetUserById(Guid id)
        {
            return await _userService.GetUserById(id);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        ///Тут нет authorize, ибо это регистрация пользователя
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AuxinaryUser _user)
        {
            var user = _mapper.Map<User>(_user);
            user.RoleId = uint.Parse(_user.RoleName);

            await _userService.AddUser(user);
            return Redirect("/Home/Login");
        }

        [Authorize]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(AuxinaryUser _user)
        {

            var user = _mapper.Map<User>(_user);
            user.RoleId = uint.Parse(_user.RoleName);

            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _userService.EditUser(user, claimModel);
            return Redirect("/Home/UserAll");
        }

        [Authorize]
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(AuxinaryUser _user)
        {
            var user = _mapper.Map<User>(_user);
            user.RoleId = uint.Parse(_user.RoleName);
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _userService.DeleteUser(user, claimModel);
            return Redirect("/Home/UserAll");
        }


    }
}
