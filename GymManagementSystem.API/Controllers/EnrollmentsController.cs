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

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        [HttpGet("{memberId}/{gymClassId}")]
        public async Task<IActionResult> GetById(int memberId, int gymClassId)
        {
            try { var enrollment = await _enrollmentService.GetEnrollmentAsync(memberId, gymClassId); return Ok(enrollment); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByMemberAsync(memberId);
            return Ok(enrollments);
        }

        [HttpGet("gymclass/{gymClassId}")]
        public async Task<IActionResult> GetByGymClass(int gymClassId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByGymClassAsync(gymClassId);
            return Ok(enrollments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentCreateDto dto)
        {
            try { var enrollment = await _enrollmentService.CreateEnrollmentAsync(dto); return CreatedAtAction(nameof(GetById), new { memberId = enrollment.MemberId, gymClassId = enrollment.GymClassId }, enrollment); }
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
