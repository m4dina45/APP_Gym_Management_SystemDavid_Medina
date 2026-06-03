using GymManagementSystem.API.DTOs;
using GymManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymClassesController : ControllerBase
    {
        private readonly IGymClassService _gymClassService;
        public GymClassesController(IGymClassService gymClassService) { _gymClassService = gymClassService; }

        [HttpGet]
        public async Task<IActionResult> GetAll() { return Ok(await _gymClassService.GetAllGymClassesAsync()); }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try { return Ok(await _gymClassService.GetGymClassByIdAsync(id)); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("trainer/{trainerId}")]
        public async Task<IActionResult> GetByTrainer(int trainerId) { return Ok(await _gymClassService.GetGymClassesByTrainerAsync(trainerId)); }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GymClassCreateDto dto)
        {
            try { var c = await _gymClassService.CreateGymClassAsync(dto); return CreatedAtAction(nameof(GetById), new { id = c.Id }, c); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GymClassUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID no coincide." });
            try { await _gymClassService.UpdateGymClassAsync(dto); return NoContent(); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _gymClassService.DeleteGymClassAsync(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
