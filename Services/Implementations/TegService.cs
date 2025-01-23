using AutoMapper;
using BigBlog.Exceptions;
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
            throw new ErrorException("AddTeg: Такой тег уже существует!");
        }

        public async Task DeleteTeg(Teg _teg, ClaimModel claimModel)
        {
            var teg = await GetTegById(_teg.Id);
            if (teg != null && (claimModel.Id == teg.UserId || claimModel.RoleName == "Администратор"))
            {
                _dbContext.Tegs.Remove(teg);
                await _dbContext.SaveChangesAsync();
            }
            throw new ErrorException("DeleteTeg: Такой тег уже существует!");
        }

        public async Task EditTeg(Teg teg, ClaimModel claimModel)
        {
            var dbTeg = await GetTegById(teg.Id);
            if (dbTeg != null &&(claimModel.Id == teg.UserId || claimModel.RoleName == "Администратор"))
            {
                var dbTegWithThatName = await _dbContext.Tegs.Where(x => x.Name == teg.Name).FirstOrDefaultAsync();
                if (dbTegWithThatName == null)
                {
                    dbTeg.Name = teg.Name;

                    await _dbContext.SaveChangesAsync();
                }
                else throw new ErrorException("EditTeg: Такой тег уже существует!");
            }
            throw new ErrorException("EditTeg: У пользователя недостатчно прав на это!");
        }

        public async Task<List<Teg>> GetAllTegs()
        {
            var x = await _dbContext.Tegs.ToListAsync();
            return x;
        }

        public async Task<Teg> GetTegById(Guid tegId)
        {
            var x = await _dbContext.Tegs.Where(x=>x.Id == tegId).FirstOrDefaultAsync();
            if(x == null) throw new ErrorException("GetTegById: Тег не найден!");
            return x;
        }
    }
}
