using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.DataAccess.Repositories
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Task<Trainer> GetByEmailAsync(string email);
        Task<IEnumerable<Trainer>> GetBySpecialtyAsync(string specialty);
    }
}
