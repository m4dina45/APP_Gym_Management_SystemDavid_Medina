using GymManagementSystem.API.DTOs;

namespace GymManagementSystem.API.Services
{
    public interface IGymClassService
    {
        Task<IEnumerable<GymClassDto>> GetAllGymClassesAsync();
        Task<GymClassDto> GetGymClassByIdAsync(int id);
        Task<GymClassDto> CreateGymClassAsync(GymClassCreateDto dto);
        Task UpdateGymClassAsync(GymClassUpdateDto dto);
        Task DeleteGymClassAsync(int id);
        Task<IEnumerable<GymClassDto>> GetGymClassesByTrainerAsync(int trainerId);
    }
}
