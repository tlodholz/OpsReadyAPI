using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly UserDbContext _context;

        public VehicleMaintenanceController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMaintenance
        // Optional query: ?vehicleId=1&unitNumber=P-204&serviceType=Oil%20Change&passedInspection=true&from=2025-01-01&to=2025-12-31
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] int? vehicleId,
            [FromQuery] string? unitNumber,
            [FromQuery] string? serviceType,
            [FromQuery] bool? passedInspection,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var q = _context.Set<VehicleMaintenance>().AsQueryable();

            if (vehicleId.HasValue) q = q.Where(m => m.VehicleId == vehicleId.Value);
            if (!string.IsNullOrWhiteSpace(unitNumber)) q = q.Where(m => EF.Functions.Like(m.UnitNumber, $"%{unitNumber}%"));
            if (!string.IsNullOrWhiteSpace(serviceType)) q = q.Where(m => EF.Functions.Like(m.ServiceType, $"%{serviceType}%"));
            if (passedInspection.HasValue) q = q.Where(m => m.PassedInspection == passedInspection.Value);
            if (from.HasValue) q = q.Where(m => m.ServiceDate >= from.Value);
            if (to.HasValue) q = q.Where(m => m.ServiceDate <= to.Value);

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/VehicleMaintenance/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Set<VehicleMaintenance>().FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/VehicleMaintenance
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleMaintenance input)
        {
            if (input == null) return BadRequest();

            var now = DateTime.UtcNow;
            input.RecordCreatedDate = now;
            input.RecordUpdatedDate = now;
            input.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            input.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<VehicleMaintenance>().Add(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = input.MaintenanceId }, input);
        }

        // PUT: api/VehicleMaintenance/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehicleMaintenance input)
        {
            if (input == null || id != input.MaintenanceId) return BadRequest();

            var stored = await _context.Set<VehicleMaintenance>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.VehicleId = input.VehicleId;
            stored.UnitNumber = input.UnitNumber;
            stored.ServiceDate = input.ServiceDate;
            stored.ServiceType = input.ServiceType;
            stored.Description = input.Description;
            stored.OdometerReading = input.OdometerReading;
            stored.IsScheduledService = input.IsScheduledService;
            stored.IsRepair = input.IsRepair;
            stored.IsUpgrade = input.IsUpgrade;
            stored.PartsReplaced = input.PartsReplaced;
            stored.LaborPerformedBy = input.LaborPerformedBy;
            stored.LaborCost = input.LaborCost;
            stored.PartsCost = input.PartsCost;
            // TotalCost is computed, do not set.
            stored.PassedInspection = input.PassedInspection;
            stored.InspectionNotes = input.InspectionNotes;
            stored.NextInspectionDue = input.NextInspectionDue;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<VehicleMaintenance>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VehicleMaintenance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<VehicleMaintenance>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<VehicleMaintenance>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}