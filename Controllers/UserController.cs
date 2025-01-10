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
        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return Redirect("/Home/Login");
        }

        [Authorize]
        [HttpPatch("EditUser")]
        public async Task EditUser(Guid id, User user)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _userService.EditUser(id, user, claimModel);
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task DeleteUser(Guid id)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _userService.DeleteUser(id, claimModel);
        }


    }
}
