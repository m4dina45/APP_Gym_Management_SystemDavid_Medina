using GymManagementSystem.API.DTOs;
using GymManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentsController(IEnrollmentService enrollmentService) { _enrollmentService = enrollmentService; }

        [HttpGet]
        public async Task<IActionResult> GetAll() { return Ok(await _enrollmentService.GetAllEnrollmentsAsync()); }

        [HttpGet("{memberId}/{gymClassId}")]
        public async Task<IActionResult> GetById(int memberId, int gymClassId)
        {
            try { return Ok(await _enrollmentService.GetEnrollmentAsync(memberId, gymClassId)); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId) { return Ok(await _enrollmentService.GetEnrollmentsByMemberAsync(memberId)); }

        [HttpGet("gymclass/{gymClassId}")]
        public async Task<IActionResult> GetByGymClass(int gymClassId) { return Ok(await _enrollmentService.GetEnrollmentsByGymClassAsync(gymClassId)); }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentCreateDto dto)
        {
            try { var e = await _enrollmentService.CreateEnrollmentAsync(dto); return CreatedAtAction(nameof(GetById), new { memberId = e.MemberId, gymClassId = e.GymClassId }, e); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{memberId}/{gymClassId}")]
        public async Task<IActionResult> Delete(int memberId, int gymClassId)
        {
            try { await _enrollmentService.DeleteEnrollmentAsync(memberId, gymClassId); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
