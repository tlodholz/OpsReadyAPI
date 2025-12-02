using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;
using System;

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

        [HttpPost]
        public async Task<IActionResult> AssignUser([FromBody] TrainingAssignment assignment)
        {
            if (assignment == null) return BadRequest();

            var exists = await _context.TrainingAssignments
                .AnyAsync(a => a.UserId == assignment.UserId && a.TrainingEventId == assignment.TrainingEventId);

            if (exists) return BadRequest("User already assigned to this event.");

            var now = DateTime.UtcNow;

            // Use a transaction so assignment + record are created atomically.
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Create the assignment first so we have the assignment.Id to link from the record.
                assignment.AssignedDate = now;
                assignment.CreatedDate = now;
                _context.TrainingAssignments.Add(assignment);
                await _context.SaveChangesAsync(); // assignment.Id is now populated

                // Create the training record and link it to the assignment via TrainingAssignmentId.
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
                await _context.SaveChangesAsync(); // record.Id populated

                // Return assignment and the created TrainingRecord id
                return Ok(new { assignment, trainingRecordId = record.Id });
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
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
