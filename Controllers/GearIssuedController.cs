using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearIssuedController : ControllerBase
    {
        private readonly UserDbContext _context;

        public GearIssuedController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/GearIssued
        // Optional query: ?officerId=123
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int? officerId)
        {
            var set = _context.Set<GearIssued>().AsQueryable();

            if (officerId.HasValue)
                set = set.Where(g => g.OfficerId == officerId.Value);

            var results = await set.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/GearIssued/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Set<GearIssued>().FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/GearIssued
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GearIssued gear)
        {
            if (gear == null) return BadRequest();

            var now = DateTime.UtcNow;
            gear.RecordCreatedDate = now;
            gear.RecordUpdatedDate = now;
            gear.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            gear.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<GearIssued>().Add(gear);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = gear.GearIssuedId }, gear);
        }

        // PUT: api/GearIssued/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GearIssued input)
        {
            if (input == null || id != input.GearIssuedId) return BadRequest();

            var stored = await _context.Set<GearIssued>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.OfficerId = input.OfficerId;
            stored.GearType = input.GearType;
            stored.GearName = input.GearName;
            stored.SerialNumber = input.SerialNumber;
            stored.IssuedDate = input.IssuedDate;
            stored.DueDate = input.DueDate;
            stored.PurchaseDate = input.PurchaseDate;
            stored.ReturnedDate = input.ReturnedDate;
            stored.Condition = input.Condition;
            stored.Status = input.Status;
            stored.Location = input.Location;
            stored.RequiresCertification = input.RequiresCertification;
            stored.CertificationType = input.CertificationType;
            stored.CertificationDate = input.CertificationDate;
            stored.CertificationExpiry = input.CertificationExpiry;
            stored.TrainingId = input.TrainingId;
            stored.GearTier = input.GearTier;
            stored.UsageRestrictions = input.UsageRestrictions;
            stored.AssignedVehicleId = input.AssignedVehicleId;
            stored.LastInspectionDate = input.LastInspectionDate;
            stored.NextInspectionDue = input.NextInspectionDue;
            stored.MaintenanceNotes = input.MaintenanceNotes;
            stored.GearImageUrl = input.GearImageUrl;
            stored.WarrantyInfo = input.WarrantyInfo;
            stored.VendorInfo = input.VendorInfo;
            stored.CostCenterCode = input.CostCenterCode;
            stored.ReplacementSchedule = input.ReplacementSchedule;
            stored.GearGroupId = input.GearGroupId;
            stored.DigitalAccessLog = input.DigitalAccessLog;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<GearIssued>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/GearIssued/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Set<GearIssued>().FindAsync(id);
            if (item == null) return NotFound();

            _context.Set<GearIssued>().Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}