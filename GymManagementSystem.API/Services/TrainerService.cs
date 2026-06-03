using AutoMapper;
using GymManagementSystem.API.DTOs;
using GymManagementSystem.DataAccess.Repositories;
using GymManagementSystem.Domain.Entities;

namespace GymManagementSystem.API.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainerDto>> GetAllTrainersAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainerDto>>(trainers);
        }

        public async Task<TrainerDto> GetTrainerByIdAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            if (trainer == null)
                throw new Exception($"Entrenador con ID {id} no encontrado.");
            return _mapper.Map<TrainerDto>(trainer);
        }

        public async Task<TrainerDto> CreateTrainerAsync(TrainerCreateDto dto)
        {
            // Validar email único
            var existingTrainer = await _trainerRepository.GetByEmailAsync(dto.Email);
            if (existingTrainer != null)
                throw new Exception($"El email {dto.Email} ya está registrado.");

            var trainer = _mapper.Map<Trainer>(dto);
            var createdTrainer = await _trainerRepository.AddAsync(trainer);
            return _mapper.Map<TrainerDto>(createdTrainer);
        }

        public async Task UpdateTrainerAsync(TrainerUpdateDto dto)
        {
            var trainer = await _trainerRepository.GetByIdAsync(dto.Id);
            if (trainer == null)
                throw new Exception($"Entrenador con ID {dto.Id} no encontrado.");

            // Validar email único (solo si cambió)
            if (trainer.Email != dto.Email)
            {
                var existingTrainer = await _trainerRepository.GetByEmailAsync(dto.Email);
                if (existingTrainer != null)
                    throw new Exception($"El email {dto.Email} ya está registrado.");
            }

            _mapper.Map(dto, trainer);
            await _trainerRepository.UpdateAsync(trainer);
        }

        public async Task DeleteTrainerAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            if (trainer == null)
                throw new Exception($"Entrenador con ID {id} no encontrado.");

            await _trainerRepository.DeleteAsync(trainer);
        }

        public async Task<IEnumerable<TrainerDto>> GetTrainersBySpecialtyAsync(string specialty)
        {
            var trainers = await _trainerRepository.GetBySpecialtyAsync(specialty);
            return _mapper.Map<IEnumerable<TrainerDto>>(trainers);
        }
    }
}
