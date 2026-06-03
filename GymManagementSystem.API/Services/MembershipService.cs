using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.DataAccess.Repositories;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MembershipDto>> GetAllMembershipsAsync()
        {
            var memberships = await _membershipRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MembershipDto>>(memberships);
        }

        public async Task<MembershipDto> GetMembershipByIdAsync(int id)
        {
            var membership = await _membershipRepository.GetByIdAsync(id);
            if (membership == null)
                throw new Exception($"Membresía con ID {id} no encontrada.");
            return _mapper.Map<MembershipDto>(membership);
        }

        public async Task<MembershipDto> CreateMembershipAsync(MembershipCreateDto dto)
        {
            // Validar precio positivo
            if (dto.MonthlyPrice <= 0)
                throw new Exception("El precio mensual debe ser positivo.");

            // Validar duración positiva
            if (dto.DurationMonths <= 0)
                throw new Exception("La duración debe ser positiva.");

            var membership = _mapper.Map<Membership>(dto);
            var createdMembership = await _membershipRepository.AddAsync(membership);
            return _mapper.Map<MembershipDto>(createdMembership);
        }

        public async Task UpdateMembershipAsync(MembershipUpdateDto dto)
        {
            var membership = await _membershipRepository.GetByIdAsync(dto.Id);
            if (membership == null)
                throw new Exception($"Membresía con ID {dto.Id} no encontrada.");

            // Validar precio positivo
            if (dto.MonthlyPrice <= 0)
                throw new Exception("El precio mensual debe ser positivo.");

            // Validar duración positiva
            if (dto.DurationMonths <= 0)
                throw new Exception("La duración debe ser positiva.");

            _mapper.Map(dto, membership);
            await _membershipRepository.UpdateAsync(membership);
        }

        public async Task DeleteMembershipAsync(int id)
        {
            var membership = await _membershipRepository.GetByIdAsync(id);
            if (membership == null)
                throw new Exception($"Membresía con ID {id} no encontrada.");

            await _membershipRepository.DeleteAsync(membership);
        }
    }
}
