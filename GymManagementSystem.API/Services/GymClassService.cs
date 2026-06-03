using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.DataAccess.Repositories;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Services
{
    public class GymClassService : IGymClassService
    {
        private readonly IGymClassRepository _gymClassRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public GymClassService(IGymClassRepository gymClassRepository, ITrainerRepository trainerRepository, IMapper mapper)
        {
            _gymClassRepository = gymClassRepository;
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GymClassDto>> GetAllGymClassesAsync()
        {
            var classes = await _gymClassRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GymClassDto>>(classes);
        }

        public async Task<GymClassDto> GetGymClassByIdAsync(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdWithTrainerAsync(id);
            if (gymClass == null)
                throw new Exception($"Clase con ID {id} no encontrada.");
            return _mapper.Map<GymClassDto>(gymClass);
        }

        public async Task<GymClassDto> CreateGymClassAsync(GymClassCreateDto dto)
        {
            // Validar que el entrenador existe
            var trainer = await _trainerRepository.GetByIdAsync(dto.TrainerId);
            if (trainer == null)
                throw new Exception($"Entrenador con ID {dto.TrainerId} no encontrado.");

            // Validar MaxCapacity > 0
            if (dto.MaxCapacity <= 0)
                throw new Exception("La capacidad máxima debe ser mayor a 0.");

            var gymClass = _mapper.Map<GymClass>(dto);
            var createdClass = await _gymClassRepository.AddAsync(gymClass);
            return _mapper.Map<GymClassDto>(createdClass);
        }

        public async Task UpdateGymClassAsync(GymClassUpdateDto dto)
        {
            var gymClass = await _gymClassRepository.GetByIdAsync(dto.Id);
            if (gymClass == null)
                throw new Exception($"Clase con ID {dto.Id} no encontrada.");

            // Validar que el entrenador existe
            var trainer = await _trainerRepository.GetByIdAsync(dto.TrainerId);
            if (trainer == null)
                throw new Exception($"Entrenador con ID {dto.TrainerId} no encontrado.");

            // Validar MaxCapacity > 0
            if (dto.MaxCapacity <= 0)
                throw new Exception("La capacidad máxima debe ser mayor a 0.");

            _mapper.Map(dto, gymClass);
            await _gymClassRepository.UpdateAsync(gymClass);
        }

        public async Task DeleteGymClassAsync(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdAsync(id);
            if (gymClass == null)
                throw new Exception($"Clase con ID {id} no encontrada.");

            await _gymClassRepository.DeleteAsync(gymClass);
        }

        public async Task<IEnumerable<GymClassDto>> GetGymClassesByTrainerAsync(int trainerId)
        {
            var classes = await _gymClassRepository.GetByTrainerIdAsync(trainerId);
            return _mapper.Map<IEnumerable<GymClassDto>>(classes);
        }
    }
}
