using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.DataAccess.Repositories;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMembershipRepository membershipRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new Exception($"Miembro con ID {id} no encontrado.");
            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateMemberAsync(MemberCreateDto dto)
        {
            // Validar email único
            var existingMember = await _memberRepository.GetByEmailAsync(dto.Email);
            if (existingMember != null)
                throw new Exception($"El email {dto.Email} ya está registrado.");

            // Validar edad mínima (14 años)
            var age = DateTime.Now.Year - dto.BirthDate.Year;
            if (dto.BirthDate.AddYears(age) > DateTime.Now)
                age--;

            if (age < 14)
                throw new Exception("El miembro debe tener al menos 14 años.");

            // Validar que la membresía existe
            var membership = await _membershipRepository.GetByIdAsync(dto.MembershipId);
            if (membership == null)
                throw new Exception($"Membresía con ID {dto.MembershipId} no encontrada.");

            var member = _mapper.Map<Member>(dto);
            var createdMember = await _memberRepository.AddAsync(member);
            return _mapper.Map<MemberDto>(createdMember);
        }

        public async Task UpdateMemberAsync(MemberUpdateDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(dto.Id);
            if (member == null)
                throw new Exception($"Miembro con ID {dto.Id} no encontrado.");

            // Validar email único (solo si cambió)
            if (member.Email != dto.Email)
            {
                var existingMember = await _memberRepository.GetByEmailAsync(dto.Email);
                if (existingMember != null)
                    throw new Exception($"El email {dto.Email} ya está registrado.");
            }

            // Validar edad mínima
            var age = DateTime.Now.Year - dto.BirthDate.Year;
            if (dto.BirthDate.AddYears(age) > DateTime.Now)
                age--;

            if (age < 14)
                throw new Exception("El miembro debe tener al menos 14 años.");

            // Validar que la membresía existe
            var membership = await _membershipRepository.GetByIdAsync(dto.MembershipId);
            if (membership == null)
                throw new Exception($"Membresía con ID {dto.MembershipId} no encontrada.");

            _mapper.Map(dto, member);
            await _memberRepository.UpdateAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new Exception($"Miembro con ID {id} no encontrado.");

            await _memberRepository.DeleteAsync(member);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersByMembershipAsync(int membershipId)
        {
            var members = await _memberRepository.GetByMembershipIdAsync(membershipId);
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }
    }
}
