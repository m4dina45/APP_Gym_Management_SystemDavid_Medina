using GymManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Data;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<Enrollment> GetByMemberAndClassAsync(int memberId, int gymClassId)
        {
            return await DbSet
                .FirstOrDefaultAsync(e => e.MemberId == memberId && e.GymClassId == gymClassId);
        }

        public async Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId)
        {
            return await DbSet
                .Where(e => e.MemberId == memberId)
                .Include(e => e.GymClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId)
        {
            return await DbSet
                .Where(e => e.GymClassId == gymClassId)
                .Include(e => e.Member)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int memberId, int gymClassId)
        {
            return await DbSet
                .AnyAsync(e => e.MemberId == memberId && e.GymClassId == gymClassId);
        }

        public override async Task<Enrollment> AddAsync(Enrollment entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public override async Task DeleteAsync(Enrollment entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
