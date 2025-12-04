using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;
using System.Threading.Tasks;

namespace OpsReadyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingAssignmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainingAssignmentController(AppDbContext context)
        {
            _context = context;
        }

        // Accepts a list of assignments, skips any already present (UserId + TrainingEventId)
        [HttpPost]
        public async Task<IActionResult> AssignUser([FromBody] List<TrainingAssignment> assignments)
        {
            if (assignments == null || assignments.Count == 0) return BadRequest();

            var now = DateTime.UtcNow;
            var created = new List<object>();
            var skipped = new List<object>();

            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Process each assignment; skip when UserId+TrainingEventId already exists
                foreach (var assignment in assignments)
                {
                    if (assignment == null)
                    {
                        continue;
                    }

                    var exists = await _context.TrainingAssignments
                        .AnyAsync(a => a.UserId == assignment.UserId && a.TrainingEventId == assignment.TrainingEventId);

                    if (exists)
                    {
                        skipped.Add(new { assignment.UserId, assignment.TrainingEventId });
                        continue;
                    }

                    // create assignment first to obtain Id for FK
                    assignment.AssignedDate = now;
                    assignment.CreatedDate = now;
                    _context.TrainingAssignments.Add(assignment);
                    await _context.SaveChangesAsync(); // populate assignment.Id

                    // create linked TrainingRecord
                    var record = new TrainingRecord
                    {
                        TrainingAssignmentId = assignment.Id,
                        AssignedBy = assignment.AssignedByUserId.ToString(),
                        Attendance = string.Empty,
                        Status = string.Empty,
                        TrainingOutcome = string.Empty,
                        Score = string.Empty,
                        CertificationNumber = string.Empty,
                        SkillLevelAchieved = string.Empty,
                        ProficiencyLevel = string.Empty,
                        Notes = string.Empty,
                        HoursCompleted = 0,
                        Completed = false,
                        Strengths = string.Empty,
                        AreasForImprovement = string.Empty,
                        FollowUpRequired = false,
                        EvaluatorId = 0,
                        Evaluator = string.Empty,
                        EvaluationComments = string.Empty,
                        EvaluationType = string.Empty,
                        OfficialComments = string.Empty,
                        EnrollmentDate = now,
                        EvaluationDate = now,
                        FollowUpDate = now,
                        ExpirationDate = DateOnly.FromDateTime(now),
                        CertificationIssuedDate = now,
                        CertificationExpiryDate = now,
                        CompletionDate = now,
                        RecordCreatedBy = User?.Identity?.Name ?? "system",
                        RecordCreatedDate = now,
                        RecordUpdatedBy = User?.Identity?.Name ?? "system",
                        RecordUpdatedDate = now
                    };

                    _context.TrainingRecords.Add(record);
                    await _context.SaveChangesAsync(); // populate record.Id

                    created.Add(new { assignment.Id, assignment.UserId, assignment.TrainingEventId, trainingRecordId = record.Id });
                }

                await tx.CommitAsync();

                return Ok(new { created, skipped });
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        // GET: api/TrainingAssignment/event/{eventId}/profiles
        // Returns a list of UserProfile objects for users registered for the specified training event
        [HttpGet("event/{eventId}/profiles")]
        public async Task<IActionResult> GetUsersForEvent(int eventId)
        {
            // get distinct user ids assigned to the event
            var userIds = await _context.TrainingAssignments
                .Where(a => a.TrainingEventId == eventId)
                .Select(a => a.UserId)
                .Distinct()
                .ToListAsync();

            if (userIds == null || !userIds.Any())
            {
                return Ok(new List<UserProfile>());
            }

            var profiles = await _context.UserProfiles
                .Where(p => p.UserId != null && userIds.Contains(p.UserId.Value))
                .ToListAsync();

            return Ok(profiles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] TrainingAssignment update)
        {
            var assignment = await _context.TrainingAssignments.FindAsync(id);
            if (assignment == null) return NotFound();

            //assignment.Status = update.Status;
            assignment.ModifiedByUserId = update.ModifiedByUserId;
            assignment.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(assignment);
        }
    }

}
