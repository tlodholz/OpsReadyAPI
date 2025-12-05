using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;
using OpsReadyAPI.Models.Dto;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingRecordController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainingRecordController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/trainingrecord/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var record = await _context.TrainingRecords.FindAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        // GET: api/trainingrecord/event/{trainingEventId}
        // Returns training records for the specified training event, with user/profile info
        [HttpGet("event/{trainingEventId}", Name = "GetByTrainingEvent")]
        public async Task<IActionResult> GetByTrainingEvent(int trainingEventId)
        {
            var query =
                from a in _context.TrainingAssignments
                where a.TrainingEventId == trainingEventId
                join r in _context.TrainingRecords on a.Id equals r.TrainingAssignmentId
                join p in _context.UserProfiles on a.UserId equals p.UserId into pgrp
                from profile in pgrp.DefaultIfEmpty()
                select new TrainingRecordWithUsersDto
                {
                    Id = r.Id,
                    TrainingAssignmentId = r.TrainingAssignmentId,
                    AssignedBy = r.AssignedBy,
                    Attendance = r.Attendance,
                    Status = r.Status,
                    TrainingOutcome = r.TrainingOutcome,
                    Score = r.Score,
                    EnrollmentDate = r.EnrollmentDate,
                    CompletionDate = r.CompletionDate,
                    RecordCreatedDate = r.RecordCreatedDate,
                    RecordUpdatedDate = r.RecordUpdatedDate,
                    User = profile == null ? null : new TrainingRecordWithUsersDto.UserProfileDto
                    {
                        Id = profile.Id ?? 0, // Fixes CS0266 and CS8629 by providing a default value if null
                        UserId = profile.UserId,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        EmailAddress = profile.EmailAddress,
                        BadgeNumber = profile.BadgeNumber
                    }
                };

            var results = await query
                .AsNoTracking()
                .Distinct()
                .ToListAsync();

            return Ok(results);
        }

        // GET: api/trainingrecord/user/{userId}
        // Returns all training records for the specified user (via TrainingAssignment -> TrainingRecord)
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var records = await _context.TrainingAssignments
                .Where(a => a.UserId == userId)
                .Join(
                    _context.TrainingRecords,
                    a => a.Id,                   // assignment.Id
                    r => r.TrainingAssignmentId, // record.TrainingAssignmentId FK
                    (a, r) => r
                )
                .AsNoTracking()
                .Distinct()
                .ToListAsync();

            return Ok(records);
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

            return CreatedAtAction(nameof(GetRecord), new { id = input.Id }, input);
        }

        // PUT: api/TrainingRecord
        // Accepts the TrainingRecord in the body; the Id inside the object is used to locate the record.
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TrainingRecord input)
        {
            if (input == null || input.Id <= 0)
                return BadRequest("Payload must include a non-zero Id.");

            var stored = await _context.Set<TrainingRecord>().FindAsync(input.Id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly (do not change PK or original creation metadata)
            stored.TrainingAssignmentId = input.TrainingAssignmentId;
            stored.AssignedBy = input.AssignedBy;
            stored.Attendance = input.Attendance;
            stored.Status = input.Status;
            stored.TrainingOutcome = input.TrainingOutcome;
            stored.Score = input.Score;
            stored.CertificationNumber = input.CertificationNumber;
            stored.SkillLevelAchieved = input.SkillLevelAchieved;
            stored.ProficiencyLevel = input.ProficiencyLevel;
            stored.Notes = input.Notes;
            stored.HoursCompleted = input.HoursCompleted;
            stored.Completed = input.Completed;
            stored.Strengths = input.Strengths;
            stored.AreasForImprovement = input.AreasForImprovement;
            stored.FollowUpRequired = input.FollowUpRequired;
            stored.EvaluatorId = input.EvaluatorId;
            stored.Evaluator = input.Evaluator;
            stored.EvaluationComments = input.EvaluationComments;
            stored.EvaluationType = input.EvaluationType;
            stored.OfficialComments = input.OfficialComments;
            stored.EnrollmentDate = input.EnrollmentDate;
            stored.EvaluationDate = input.EvaluationDate;
            stored.FollowUpDate = input.FollowUpDate;
            stored.ExpirationDate = input.ExpirationDate;
            stored.CertificationIssuedDate = input.CertificationIssuedDate;
            stored.CertificationExpiryDate = input.CertificationExpiryDate;
            stored.CompletionDate = input.CompletionDate;

            // Keep original RecordCreatedBy/RecordCreatedDate; update updated metadata
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