using GymManagementSystem.API.DTOs;

namespace GymManagementSystem.API.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto> GetEnrollmentAsync(int memberId, int gymClassId);
        Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentCreateDto dto);
        Task DeleteEnrollmentAsync(int memberId, int gymClassId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByMemberAsync(int memberId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByGymClassAsync(int gymClassId);
    }
}
