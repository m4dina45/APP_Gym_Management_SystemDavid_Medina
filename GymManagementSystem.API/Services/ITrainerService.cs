using GymManagementSystem.API.DTOs;

namespace GymManagementSystem.API.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDto>> GetAllTrainersAsync();
        Task<TrainerDto> GetTrainerByIdAsync(int id);
        Task<TrainerDto> CreateTrainerAsync(TrainerCreateDto dto);
        Task UpdateTrainerAsync(TrainerUpdateDto dto);
        Task DeleteTrainerAsync(int id);
        Task<IEnumerable<TrainerDto>> GetTrainersBySpecialtyAsync(string specialty);
    }
}
