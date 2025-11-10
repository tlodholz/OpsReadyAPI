namespace OpsReady.Models
{
    public class GearIssued
    {
        // 🧰 Core Gear Fields
        public int GearId { get; set; }
        public int OfficerId { get; set; }
        public string GearType { get; set; } // e.g., Firearm, BodyCam, Uniform
        public string GearName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string Condition { get; set; } // New, Used, Damaged, Retired
        public string Status { get; set; } // Active, InStorage, UnderRepair, Retired
        public string Location { get; set; } // Locker, Vehicle, etc.

        // 🧪 Training & Certification Linkage
        public bool RequiresCertification { get; set; }
        public string CertificationType { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? CertificationExpiry { get; set; }
        public int? TrainingId { get; set; }

        // 🛡️ Tactical & Operational Metadata
        public string GearTier { get; set; } // Core, Tactical, Advanced
        public string UsageRestrictions { get; set; }
        public int? AssignedVehicleId { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public DateTime? NextInspectionDue { get; set; }
        public string MaintenanceNotes { get; set; }

               // 🧩 Optional Extensions
        public string GearImageUrl { get; set; }
        public string WarrantyInfo { get; set; }
        public string VendorInfo { get; set; }
        public string CostCenterCode { get; set; }
        public string ReplacementSchedule { get; set; }
        public int? GearGroupId { get; set; }
        public string DigitalAccessLog { get; set; }

        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
