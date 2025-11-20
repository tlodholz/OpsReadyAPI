using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthProfileController : ControllerBase
    {
        private readonly UserDbContext _context;

        public HealthProfileController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/HealthProfile
        // Optional query: ?userId=123
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? userId)
        {
            var set = _context.Set<HealthProfile>().AsQueryable();

            if (userId.HasValue)
                set = set.Where(h => h.UserId == userId.Value);

            var results = await set.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/HealthProfile/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _context.Set<HealthProfile>().FindAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        // POST: api/HealthProfile
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthProfile profile)
        {
            if (profile == null) return BadRequest();

            var now = DateTime.UtcNow;
            profile.RecordCreatedDate = now;
            profile.RecordUpdatedDate = now;
            profile.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            profile.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<HealthProfile>().Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = profile.HealthProfileId }, profile);
        }

        // PUT: api/HealthProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HealthProfile input)
        {
            if (input == null || id != input.HealthProfileId) return BadRequest();

            var stored = await _context.Set<HealthProfile>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.UserId = input.UserId;
            stored.BloodType = input.BloodType;
            stored.Allergies = input.Allergies;
            stored.Medications = input.Medications;
            stored.MedicalConditions = input.MedicalConditions;
            stored.PrimaryPhysicianName = input.PrimaryPhysicianName;
            stored.PrimaryPhysicianPhone = input.PrimaryPhysicianPhone;
            stored.InsuranceProvider = input.InsuranceProvider;
            stored.InsurancePolicyNumber = input.InsurancePolicyNumber;
            stored.RecentInjuries = input.RecentInjuries;
            stored.LastPhysicalExamDate = input.LastPhysicalExamDate;
            stored.Notes = input.Notes;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<HealthProfile>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/HealthProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.Set<HealthProfile>().FindAsync(id);
            if (profile == null) return NotFound();

            _context.Set<HealthProfile>().Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}