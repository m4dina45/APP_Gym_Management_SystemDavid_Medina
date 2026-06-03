using GymManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Data;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<Trainer> GetByEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<IEnumerable<Trainer>> GetBySpecialtyAsync(string specialty)
        {
            return await DbSet
                .Where(t => t.Specialty == specialty)
                .ToListAsync();
        }
    }
}
