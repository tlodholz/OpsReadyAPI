using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingRecordController : ControllerBase
    {
        private readonly UserDbContext _context;

        public TrainingRecordController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingRecord
        // Optional query: ?userId=1&trainingEventId=2&completed=true&evaluatorId=5&from=2025-01-01&to=2025-12-31
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] int? userId,
            [FromQuery] int? trainingEventId,
            [FromQuery] bool? completed,
            [FromQuery] int? evaluatorId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var q = _context.Set<TrainingRecord>().AsQueryable();

            if (userId.HasValue) q = q.Where(r => r.UserId == userId.Value);
            if (trainingEventId.HasValue) q = q.Where(r => r.TrainingEventId == trainingEventId.Value);
            if (completed.HasValue) q = q.Where(r => r.Completed == completed.Value);
            if (evaluatorId.HasValue) q = q.Where(r => r.EvaluatorId == evaluatorId.Value);
            if (from.HasValue) q = q.Where(r => r.CompletionDate >= from.Value);
            if (to.HasValue) q = q.Where(r => r.CompletionDate <= to.Value);

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/TrainingRecord/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var rec = await _context.Set<TrainingRecord>().FindAsync(id);
            if (rec == null) return NotFound();
            return Ok(rec);
        }

        // POST: api/TrainingRecord
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrainingRecord input)
        {
            if (input == null) return BadRequest();

            var now = DateTime.UtcNow;
            input.RecordCreatedDate = now;
            input.RecordUpdatedDate = now;
            input.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            input.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<TrainingRecord>().Add(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = input.TrainingRecordId }, input);
        }

        // PUT: api/TrainingRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainingRecord input)
        {
            if (input == null || id != input.TrainingRecordId) return BadRequest();

            var stored = await _context.Set<TrainingRecord>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.TrainingEventId = input.TrainingEventId;
            stored.UserId = input.UserId;
            stored.CompletionDate = input.CompletionDate;
            stored.Status = input.Status;
            stored.TrainingOutcome = input.TrainingOutcome;
            stored.Score = input.Score;
            stored.ProficiencyLevel = input.ProficiencyLevel;
            stored.Notes = input.Notes;
            stored.HoursCompleted = input.HoursCompleted;
            stored.Completed = input.Completed;
            stored.ExpirationDate = input.ExpirationDate;
            stored.Strengths = input.Strengths;
            stored.AreasForImprovement = input.AreasForImprovement;
            stored.FollowUpRequired = input.FollowUpRequired;
            stored.FollowUpDate = input.FollowUpDate;
            stored.EvaluatorId = input.EvaluatorId;
            stored.EvaluatorBadgeNumber = input.EvaluatorBadgeNumber;
            stored.EvaluationDate = input.EvaluationDate;
            stored.EvaluationType = input.EvaluationType;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<TrainingRecord>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TrainingRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<TrainingRecord>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<TrainingRecord>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}