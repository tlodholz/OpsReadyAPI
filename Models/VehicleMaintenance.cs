using static OpsReady.Models.Vehicles;

namespace OpsReady.Models
{
    public class VehicleMaintenanceLog
    {
        // 🔗 Identity & Linkage
        public int MaintenanceId { get; set; }
        public int VehicleId { get; set; }
        public string UnitNumber { get; set; } // Optional redundancy for quick lookup

        // 🛠️ Maintenance Details
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; } // e.g., Oil Change, Tire Rotation, Brake Repair
        public string Description { get; set; } // Detailed notes on what was done
        public int OdometerReading { get; set; }
        public bool IsScheduledService { get; set; } // True if part of planned maintenance
        public bool IsRepair { get; set; } // True if due to damage or malfunction
        public bool IsUpgrade { get; set; } // True if gear or system was enhanced

        // 🧰 Parts & Labor
        public string PartsReplaced { get; set; } // Comma-separated or JSON list
        public string LaborPerformedBy { get; set; } // Technician or vendor name
        public decimal LaborCost { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalCost => LaborCost + PartsCost;

        // 🧪 Inspection & Readiness
        public bool PassedInspection { get; set; }
        public string InspectionNotes { get; set; }
        public DateTime? NextInspectionDue { get; set; }

        // 📈 Audit & Accountability
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
