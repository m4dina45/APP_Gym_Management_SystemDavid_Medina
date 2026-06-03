using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        Task<Membership> GetByNameAsync(string name);
        Task<IEnumerable<Membership>> GetWithMembersAsync();
    }
}
