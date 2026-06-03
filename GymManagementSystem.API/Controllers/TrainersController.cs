using GymManagementSystem.API.DTOs;
using GymManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        public TrainersController(ITrainerService trainerService) { _trainerService = trainerService; }

        [HttpGet]
        public async Task<IActionResult> GetAll() { return Ok(await _trainerService.GetAllTrainersAsync()); }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try { return Ok(await _trainerService.GetTrainerByIdAsync(id)); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainerCreateDto dto)
        {
            try { var t = await _trainerService.CreateTrainerAsync(dto); return CreatedAtAction(nameof(GetById), new { id = t.Id }, t); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID no coincide." });
            try { await _trainerService.UpdateTrainerAsync(dto); return NoContent(); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _trainerService.DeleteTrainerAsync(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
