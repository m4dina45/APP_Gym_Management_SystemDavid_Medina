using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<Enrollment> GetByMemberAndClassAsync(int memberId, int gymClassId);
        Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId);
        Task<bool> ExistsAsync(int memberId, int gymClassId);
    }
}
