using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingEventController : ControllerBase
    {
        private readonly UserDbContext _context;

        public TrainingEventController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingEvent
        // Optional query: ?from=2025-01-01&to=2025-12-31&title=range
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? title)
        {
            var q = _context.Set<TrainingEvent>().AsQueryable();

            if (from.HasValue)
                q = q.Where(te => te.StartDate >= from.Value);

            if (to.HasValue)
                q = q.Where(te => te.EndDate <= to.Value);

            if (!string.IsNullOrWhiteSpace(title))
                q = q.Where(te => EF.Functions.Like(te.Title, $"%{title}%"));

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/TrainingEvent/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _context.Set<TrainingEvent>().FindAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        // POST: api/TrainingEvent
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainingEvent input)
        {
            if (input == null) return BadRequest();

            var now = DateTime.UtcNow;
            input.RecordCreatedDate = now;
            input.RecordUpdatedDate = now;
            input.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            input.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<TrainingEvent>().Add(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = input.TrainingEventId }, input);
        }

        // PUT: api/TrainingEvent/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainingEvent input)
        {
            if (input == null || id != input.TrainingEventId) return BadRequest();

            var stored = await _context.Set<TrainingEvent>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.Title = input.Title;
            stored.Description = input.Description;
            stored.RecordedDate = input.RecordedDate;
            stored.StartDate = input.StartDate;
            stored.EndDate = input.EndDate;
            stored.Location = input.Location;
            stored.Instructor = input.Instructor;
            stored.DurationHours = input.DurationHours;
            stored.CertificationIssued = input.CertificationIssued;
            stored.CertificationIssuedBy = input.CertificationIssuedBy;
            stored.CertificationExpiryDate = input.CertificationExpiryDate;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<TrainingEvent>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TrainingEvent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<TrainingEvent>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<TrainingEvent>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}