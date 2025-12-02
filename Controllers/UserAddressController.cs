using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserAddressController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAddress
        // Optional query: ?userId=123&isPrimary=true
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? userId, [FromQuery] bool? isPrimary)
        {
            var q = _context.Set<UserAddress>().AsQueryable();

            if (userId.HasValue) q = q.Where(a => a.UserId == userId.Value);
            if (isPrimary.HasValue) q = q.Where(a => a.IsPrimary == isPrimary.Value);

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/UserAddress/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var addr = await _context.Set<UserAddress>().FindAsync(id);
            if (addr == null) return NotFound();
            return Ok(addr);
        }

        // POST: api/UserAddress
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddress input)
        {
            if (input == null) return BadRequest();

            var now = DateTime.UtcNow;
            input.RecordCreatedDate = now;
            input.RecordUpdatedDate = now;
            input.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            input.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<UserAddress>().Add(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = input.AddressId }, input);
        }

        // PUT: api/UserAddress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserAddress input)
        {
            if (input == null || id != input.AddressId) return BadRequest();

            var stored = await _context.Set<UserAddress>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.UserId = input.UserId;
            stored.Address1 = input.Address1;
            stored.Address2 = input.Address2;
            stored.Address3 = input.Address3;
            stored.City = input.City;
            stored.State = input.State;
            stored.ZipCode = input.ZipCode;
            stored.Country = input.Country;
            stored.Phone = input.Phone;
            stored.IsPrimary = input.IsPrimary;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<UserAddress>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<UserAddress>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<UserAddress>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}