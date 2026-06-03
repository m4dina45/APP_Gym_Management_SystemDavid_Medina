using GymManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.DataAccess.Data;

namespace GymManagementSystem.DataAccess.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext context) : base(context)
        {
        }

        public async Task<Member> GetByEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<IEnumerable<Member>> GetByMembershipIdAsync(int membershipId)
        {
            return await DbSet
                .Where(m => m.MembershipId == membershipId)
                .ToListAsync();
        }
    }
}
