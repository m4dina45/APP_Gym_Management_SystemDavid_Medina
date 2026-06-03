using GymManagementSystem.API.DTOs;
using GymManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipsController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipsController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var memberships = await _membershipService.GetAllMembershipsAsync();
            return Ok(memberships);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try { var membership = await _membershipService.GetMembershipByIdAsync(id); return Ok(membership); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MembershipCreateDto dto)
        {
            try { var membership = await _membershipService.CreateMembershipAsync(dto); return CreatedAtAction(nameof(GetById), new { id = membership.Id }, membership); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MembershipUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID no coincide." });
            try { await _membershipService.UpdateMembershipAsync(dto); return NoContent(); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _membershipService.DeleteMembershipAsync(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
