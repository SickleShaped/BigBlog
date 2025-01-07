using AutoMapper;
using BigBlog.Models;
using BigBlog.Models.Db;
using BigBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBlog.Services.Implementations
{
    public class TegService:ITegService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;

        public TegService(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddTeg(Teg teg, ClaimModel claimModel)
        {
            teg.Id = Guid.NewGuid();
            teg.UserId = claimModel.Id;

            var dbteg = await _dbContext.Tegs.Where(x=>x.Name == teg.Name).FirstOrDefaultAsync();
            if (dbteg == null)
            {
                await _dbContext.Tegs.AddAsync(teg);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteTeg(Guid tegId, ClaimModel claimModel)
        {
            var teg = await GetTegById(tegId);
            if (teg != null && (claimModel.Id == teg.UserId || claimModel.RoleName == "Администратор"))
            {
                _dbContext.Tegs.Remove(teg);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditTeg(Guid tegId, Teg teg, ClaimModel claimModel)
        {
            var dbTeg = await GetTegById(tegId);
            if (dbTeg != null &&(claimModel.Id == teg.UserId || claimModel.RoleName == "Администратор"))
            {
                var dbTegWithThatName = await _dbContext.Tegs.Where(x => x.Name == teg.Name).FirstOrDefaultAsync();
                if (dbTegWithThatName == null)
                {
                    dbTeg.Name = teg.Name;

                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Teg>> GetAllTegs()
        {
            var x = await _dbContext.Tegs.ToListAsync();
            return x;
        }

        public async Task<Teg> GetTegById(Guid tegId)
        {
            var x = await _dbContext.Tegs.Where(x=>x.Id == tegId).FirstOrDefaultAsync();
            return x;
        }
    }
}
