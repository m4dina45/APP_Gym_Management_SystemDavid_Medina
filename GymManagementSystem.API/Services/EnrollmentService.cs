using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.DataAccess.Repositories;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGymClassRepository _gymClassRepository;
        private readonly IMapper _mapper;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IMemberRepository memberRepository,
            IGymClassRepository gymClassRepository,
            IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _memberRepository = memberRepository;
            _gymClassRepository = gymClassRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto> GetEnrollmentAsync(int memberId, int gymClassId)
        {
            var enrollment = await _enrollmentRepository.GetByMemberAndClassAsync(memberId, gymClassId);
            if (enrollment == null)
                throw new Exception($"Inscripción no encontrada.");
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(EnrollmentCreateDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(dto.MemberId);
            if (member == null)
                throw new Exception($"Miembro con ID {dto.MemberId} no encontrado.");

            var gymClass = await _gymClassRepository.GetByIdAsync(dto.GymClassId);
            if (gymClass == null)
                throw new Exception($"Clase con ID {dto.GymClassId} no encontrada.");

            var existingEnrollment = await _enrollmentRepository.ExistsAsync(dto.MemberId, dto.GymClassId);
            if (existingEnrollment)
                throw new Exception($"El miembro ya está inscrito en esta clase.");

            var enrollmentsInClass = await _enrollmentRepository.GetByGymClassIdAsync(dto.GymClassId);
            if (enrollmentsInClass.Count() >= gymClass.MaxCapacity)
                throw new Exception($"La clase ha alcanzado su capacidad máxima.");

            var enrollment = new Enrollment
            {
                MemberId = dto.MemberId,
                GymClassId = dto.GymClassId,
                EnrollmentDate = DateTime.Now
            };

            var createdEnrollment = await _enrollmentRepository.AddAsync(enrollment);
            return _mapper.Map<EnrollmentDto>(createdEnrollment);
        }

        public async Task DeleteEnrollmentAsync(int memberId, int gymClassId)
        {
            var enrollment = await _enrollmentRepository.GetByMemberAndClassAsync(memberId, gymClassId);
            if (enrollment == null)
                throw new Exception($"Inscripción no encontrada.");

            await _enrollmentRepository.DeleteAsync(enrollment);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByMemberAsync(int memberId)
        {
            var enrollments = await _enrollmentRepository.GetByMemberIdAsync(memberId);
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByGymClassAsync(int gymClassId)
        {
            var enrollments = await _enrollmentRepository.GetByGymClassIdAsync(gymClassId);
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }
    }
}
