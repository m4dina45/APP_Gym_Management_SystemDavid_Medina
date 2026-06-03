using GymManagementSystem.API.DTOs;

namespace GymManagementSystem.API.Services
{
    public interface IMembershipService
    {
        Task<IEnumerable<MembershipDto>> GetAllMembershipsAsync();
        Task<MembershipDto> GetMembershipByIdAsync(int id);
        Task<MembershipDto> CreateMembershipAsync(MembershipCreateDto dto);
        Task UpdateMembershipAsync(MembershipUpdateDto dto);
        Task DeleteMembershipAsync(int id);
    }
}
