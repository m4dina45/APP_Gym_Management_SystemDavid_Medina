using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface IGymClassRepository : IGenericRepository<GymClass>
    {
        Task<GymClass> GetByIdWithTrainerAsync(int id);
        Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId);
        Task<IEnumerable<GymClass>> GetWithEnrollmentsAsync();
    }
}
