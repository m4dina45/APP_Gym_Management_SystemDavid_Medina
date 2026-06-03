using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> GetByEmailAsync(string email);
        Task<IEnumerable<Member>> GetByMembershipIdAsync(int membershipId);
    }
}
