using GymManagementSystem.API.DTOs;
using GymManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService) { _memberService = memberService; }

        [HttpGet]
        public async Task<IActionResult> GetAll() { return Ok(await _memberService.GetAllMembersAsync()); }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try { return Ok(await _memberService.GetMemberByIdAsync(id)); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpGet("membership/{membershipId}")]
        public async Task<IActionResult> GetByMembership(int membershipId) { return Ok(await _memberService.GetMembersByMembershipAsync(membershipId)); }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberCreateDto dto)
        {
            try { var m = await _memberService.CreateMemberAsync(dto); return CreatedAtAction(nameof(GetById), new { id = m.Id }, m); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MemberUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID no coincide." });
            try { await _memberService.UpdateMemberAsync(dto); return NoContent(); }
            catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _memberService.DeleteMemberAsync(id); return NoContent(); }
            catch (Exception ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
