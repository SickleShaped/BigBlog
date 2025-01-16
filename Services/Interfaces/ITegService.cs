using BigBlog.Models;
using BigBlog.Models.Db;

namespace BigBlog.Services.Interfaces
{
    public interface ITegService
    {
        Task<Teg> GetTegById(Guid tegId);
        Task<List<Teg>> GetAllTegs();
        Task AddTeg(Teg teg, ClaimModel claimModel);
        Task EditTeg(Teg teg, ClaimModel claimModel);
        Task DeleteTeg(Teg teg, ClaimModel claimModel);
    }
}
