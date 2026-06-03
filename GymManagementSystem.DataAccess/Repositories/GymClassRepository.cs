using GymManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Data;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class GymClassRepository : GenericRepository<GymClass>, IGymClassRepository
    {
        public GymClassRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<GymClass> GetByIdWithTrainerAsync(int id)
        {
            return await DbSet
                .Include(g => g.Trainer)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId)
        {
            return await DbSet
                .Where(g => g.TrainerId == trainerId)
                .Include(g => g.Trainer)
                .ToListAsync();
        }

        public async Task<IEnumerable<GymClass>> GetWithEnrollmentsAsync()
        {
            return await DbSet
                .Include(g => g.Trainer)
                .Include(g => g.Enrollments)
                .ToListAsync();
        }
    }
}
