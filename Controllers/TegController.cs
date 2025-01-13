using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BigBlog.Controllers
{
    public class TegController : Controller
    {
        private readonly ITegService _tegService;
        public TegController(ITegService tegService)
        {
            _tegService = tegService;
        }

        [Authorize]
        [HttpGet("GetTegById")]
        public async Task<Teg> GetTegById(Guid id)
        {
            return await _tegService.GetTegById(id);
        }

        [Authorize]
        [HttpGet("GetAllTegs")]
        public async Task<List<Teg>> GetAllTegs()
        {
            return await _tegService.GetAllTegs();
        }

        [Authorize]
        [HttpPost("AddTeg")]
        public async Task AddTeg(Teg teg)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _tegService.AddTeg(teg, claimModel);
        }

        [Authorize]
        [HttpPatch("EditTeg")]
        public async Task EditTeg(Guid id, Teg teg)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _tegService.EditTeg(id, teg, claimModel);
        }

        [Authorize]
        [HttpDelete("DeleteTeg")]
        public async Task DeleteTeg(Guid id)
        {
            var claimId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var claimRole = User.FindFirst(ClaimTypes.Role)?.Value;
            ClaimModel claimModel = new ClaimModel() { Id = claimId, RoleName = claimRole };

            await _tegService.DeleteTeg(id, claimModel);
        }
    }
}
