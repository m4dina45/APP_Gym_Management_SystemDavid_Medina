using GymManagementSystem.API.DTOs;

namespace GymManagementSystem.API.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<MemberDto> CreateMemberAsync(MemberCreateDto dto);
        Task UpdateMemberAsync(MemberUpdateDto dto);
        Task DeleteMemberAsync(int id);
        Task<IEnumerable<MemberDto>> GetMembersByMembershipAsync(int membershipId);
    }
}
