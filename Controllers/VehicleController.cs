using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsReady.Data;
using OpsReady.Models;

namespace OpsReady.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly UserDbContext _context;

        public VehicleController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicle
        // Optional query: ?unitNumber=P-204&vin=...&assignedOfficerId=1&status=Active&isOperational=true
        [HttpGet]
        public async Task<IActionResult> List(
            [FromQuery] string? unitNumber,
            [FromQuery] string? vin,
            [FromQuery] int? assignedOfficerId,
            [FromQuery] string? status,
            [FromQuery] int? vehicleGroupId,
            [FromQuery] bool? isOperational)
        {
            var q = _context.Set<Vehicle>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(unitNumber))
                q = q.Where(v => EF.Functions.Like(v.UnitNumber, $"%{unitNumber}%"));

            if (!string.IsNullOrWhiteSpace(vin))
                q = q.Where(v => EF.Functions.Like(v.VIN, $"%{vin}%"));

            if (assignedOfficerId.HasValue)
                q = q.Where(v => v.AssignedOfficerId == assignedOfficerId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                q = q.Where(v => v.Status == status);

            if (vehicleGroupId.HasValue)
                q = q.Where(v => v.VehicleGroupId == vehicleGroupId.Value);

            if (isOperational.HasValue)
                q = q.Where(v => v.IsOperational == isOperational.Value);

            var results = await q.AsNoTracking().ToListAsync();
            return Ok(results);
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vehicle = await _context.Set<Vehicle>().FindAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        // POST: api/Vehicle
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vehicle input)
        {
            if (input == null) return BadRequest();

            var now = DateTime.UtcNow;
            input.RecordCreatedDate = now;
            input.RecordUpdatedDate = now;
            input.RecordCreatedBy ??= User?.Identity?.Name ?? "system";
            input.RecordUpdatedBy ??= User?.Identity?.Name ?? "system";

            _context.Set<Vehicle>().Add(input);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = input.VehicleId }, input);
        }

        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vehicle input)
        {
            if (input == null || id != input.VehicleId) return BadRequest();

            var stored = await _context.Set<Vehicle>().FindAsync(id);
            if (stored == null) return NotFound();

            // Map updatable fields explicitly
            stored.UnitNumber = input.UnitNumber;
            stored.VIN = input.VIN;
            stored.LicensePlate = input.LicensePlate;
            stored.Make = input.Make;
            stored.Model = input.Model;
            stored.Year = input.Year;
            stored.Color = input.Color;
            stored.DepartmentalAssetTag = input.DepartmentalAssetTag;
            stored.VehicleImageUrl = input.VehicleImageUrl;
            stored.PurchaseDate = input.PurchaseDate;
            stored.PurchasePrice = input.PurchasePrice;
            stored.FuelType = input.FuelType;
            stored.VehicleType = input.VehicleType;
            stored.IsK9Unit = input.IsK9Unit;
            stored.Status = input.Status;
            stored.AssignedOfficerId = input.AssignedOfficerId;
            stored.AssignedUnit = input.AssignedUnit;
            stored.AssignmentDate = input.AssignmentDate;
            stored.ReturnDate = input.ReturnDate;
            stored.CurrentOdometer = input.CurrentOdometer;
            stored.LastServiceDate = input.LastServiceDate;
            stored.NextServiceDue = input.NextServiceDue;
            stored.MaintenanceId = input.MaintenanceId;
            stored.IsOperational = input.IsOperational;
            stored.HasBodyCamDock = input.HasBodyCamDock;
            stored.HasInCarCamera = input.HasInCarCamera;
            stored.HasRifleMount = input.HasRifleMount;
            stored.HasK9Cage = input.HasK9Cage;
            stored.RadioId = input.RadioId;
            stored.MDTSerial = input.MDTSerial;
            stored.DecommissionedDate = input.DecommissionedDate;
            stored.DecommissionReason = input.DecommissionReason;
            stored.Notes = input.Notes;
            stored.WarrantyInfo = input.WarrantyInfo;
            stored.InsurancePolicyNumber = input.InsurancePolicyNumber;
            stored.InsuranceExpiryDate = input.InsuranceExpiryDate;
            stored.GPSUnitId = input.GPSUnitId;
            stored.CostCenterCode = input.CostCenterCode;
            stored.VendorInfo = input.VendorInfo;
            stored.ReplacementSchedule = input.ReplacementSchedule;
            stored.VehicleGroupId = input.VehicleGroupId;
            stored.DigitalAccessLog = input.DigitalAccessLog;
            stored.HasEmergencyLights = input.HasEmergencyLights;
            stored.HasSiren = input.HasSiren;
            stored.HasPushBumper = input.HasPushBumper;
            stored.CustomDecals = input.CustomDecals;
            stored.InteriorConfiguration = input.InteriorConfiguration;
            stored.ExteriorConfiguration = input.ExteriorConfiguration;
            stored.SpecializedEquipment = input.SpecializedEquipment;
            stored.CommunicationSystems = input.CommunicationSystems;
            stored.SafetyFeatures = input.SafetyFeatures;
            stored.PerformanceUpgrades = input.PerformanceUpgrades;

            stored.RecordUpdatedBy = User?.Identity?.Name ?? "system";
            stored.RecordUpdatedDate = DateTime.UtcNow;

            _context.Set<Vehicle>().Update(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stored = await _context.Set<Vehicle>().FindAsync(id);
            if (stored == null) return NotFound();

            _context.Set<Vehicle>().Remove(stored);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}