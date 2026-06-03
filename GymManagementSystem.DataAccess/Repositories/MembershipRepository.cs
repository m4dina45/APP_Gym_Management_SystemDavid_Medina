using GymManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Data;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        public MembershipRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<Membership> GetByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<Membership>> GetWithMembersAsync()
        {
            return await DbSet
                .Include(m => m.Members)
                .ToListAsync();
        }
    }
}
