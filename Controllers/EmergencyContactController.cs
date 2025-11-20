using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactController : ControllerBase
    {
        private readonly UserDbContext _context;

        public EmergencyContactController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/EmergencyContact
        // Optional query: ?userId=123
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? userId)
        {
            var set = _context.Set<EmergencyContact>().AsQueryable();

            if (userId.HasValue)
                set = set.Where(ec => ec.UserId == userId.Value);

            var results = await set.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/EmergencyContact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _context.Set<EmergencyContact>().FindAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        // POST: api/EmergencyContact
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmergencyContact contact)
        {
            if (contact == null) return BadRequest();

            // set audit fields
            var now = DateTime.UtcNow;
            contact.RecordCreatedDate = now;
            contact.RecordUpdatedDate = now;
            contact.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            contact.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<EmergencyContact>().Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = contact.EmergencyContactId }, contact);
        }

        // PUT: api/EmergencyContact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmergencyContact input)
        {
            if (input == null || id != input.EmergencyContactId) return BadRequest();

            var stored = await _context.Set<EmergencyContact>().FindAsync(id);
            if (stored == null) return NotFound();

            // map updatable fields (explicit to avoid overwriting audit fields unintentionally)
            stored.UserId = input.UserId;
            stored.FirstName = input.FirstName;
            stored.LastName = input.LastName;
            stored.IsPrimary = input.IsPrimary;
            stored.Relationship = input.Relationship;
            stored.PhoneNumber = input.PhoneNumber;
            stored.PhoneNumberIsMobile = input.PhoneNumberIsMobile;
            stored.EmailAddress = input.EmailAddress;
            stored.Address1 = input.Address1;
            stored.Address2 = input.Address2;
            stored.Address3 = input.Address3;
            stored.City = input.City;
            stored.State = input.State;
            stored.ZipCode = input.ZipCode;
            stored.Country = input.Country;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<EmergencyContact>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/EmergencyContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Set<EmergencyContact>().FindAsync(id);
            if (contact == null) return NotFound();

            _context.Set<EmergencyContact>().Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
