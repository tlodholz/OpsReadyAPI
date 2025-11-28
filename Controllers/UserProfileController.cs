using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserProfileController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile/all
        // Returns all user profiles (no filtering)
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _context.Set<UserProfile>().AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/UserProfile
        // Optional query: ?userId=1&badgeNumber=ABC&name=smith&isActiveDuty=true
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] int? userId)
        {
            var q = _context.Set<UserProfile>().AsQueryable();

            if (userId.HasValue) q = q.Where(u => u.UserId == userId.Value);

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/UserProfile/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var profile = await _context.Set<UserProfile>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (profile == null) return NotFound();
            return Ok(profile);
        }

        // POST: api/UserProfile
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserProfile profile)
        {
            if (profile == null) return BadRequest();
            profile.Id = null; // Ensure PK is zero for new record

            var now = DateTime.UtcNow;
            profile.RecordCreatedDate = now;
            profile.RecordUpdatedDate = now;
            profile.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            profile.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<UserProfile>().Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { userId = profile.UserId }, profile);
        }

        // PUT: api/UserProfile
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserProfile input)
        {
            if (input == null) return BadRequest();

            // Require the primary key to be present in the payload
            if (input.Id == 0) return BadRequest("Id must be provided and non-zero.");

            // Optionally require UserId as well if your logic depends on it:
            // if (input.UserId == 0) return BadRequest("UserId must be provided and non-zero.");

            var stored = await _context.Set<UserProfile>().FindAsync(input.Id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly (do not change PK/UserId unless intentionally allowed)
            stored.FirstName = input.FirstName;
            stored.LastName = input.LastName;
            stored.PreferredName = input.PreferredName;
            stored.BadgeNumber = input.BadgeNumber;
            stored.Position = input.Position;
            stored.Department = input.Department;
            stored.Location = input.Location;
            stored.Address1 = input.Address1;
            stored.Address2 = input.Address2;
            stored.City = input.City;
            stored.State = input.State;
            stored.ZipCode = input.ZipCode;
            stored.Skills = input.Skills;
            stored.Status = input.Status;
            stored.Rank = input.Rank;
            stored.Bio = input.Bio;
            stored.PhoneNumber = input.PhoneNumber;
            stored.EmailAddress = input.EmailAddress;
            stored.DateOfBirth = input.DateOfBirth;
            stored.DateOfHire = input.DateOfHire;
            stored.Gender = input.Gender;
            stored.Ethnicity = input.Ethnicity;
            stored.ShiftSchedule = input.ShiftSchedule;
            stored.SupervisorBadgeNumber = input.SupervisorBadgeNumber;
            stored.StationLocation = input.StationLocation;
            stored.LegalRestrictions = input.LegalRestrictions;
            stored.ExemptionsGranted = input.ExemptionsGranted;
            stored.DisciplinaryActions = input.DisciplinaryActions;
            stored.UseOfForceClearance = input.UseOfForceClearance;
            stored.LanguageFluency = input.LanguageFluency;
            stored.SpecialClearances = input.SpecialClearances;
            stored.IsActiveDuty = input.IsActiveDuty;
            stored.CommandingOfficerBadgeNumber = input.CommandingOfficerBadgeNumber;
            stored.Notes = input.Notes;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<UserProfile>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<UserProfile>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<UserProfile>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}