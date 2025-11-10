namespace OpsReady.Models
{
    public class Vehicles
    {
        public class Vehicle
        {
            // 🚘 Core Identity
            public int VehicleId { get; set; }
            public string UnitNumber { get; set; } // e.g., "P-204", "K9-12"
            public string VIN { get; set; }
            public string LicensePlate { get; set; }
            public string Make { get; set; } // Ford, Dodge, etc.
            public string Model { get; set; } // Explorer, Charger, etc.
            public int Year { get; set; }
            public string Color { get; set; }
            public string DepartmentalAssetTag { get; set; }
            public string VehicleImageUrl { get; set; } 
            public DateTime PurchaseDate { get; set; }
            public decimal PurchasePrice { get; set; }
            public string FuelType { get; set; } // Gasoline, Diesel, Hybrid, Electric

            // 🧭 Assignment & Status
            public string VehicleType { get; set; } // Patrol, K9, SWAT, Admin
            public string IsK9Unit { get; set; }
            public string Status { get; set; } // Active, InMaintenance, Retired
            public int? AssignedOfficerId { get; set; }
            public string AssignedUnit { get; set; } // e.g., "Night Shift", "Traffic Division"
            public DateTime? AssignmentDate { get; set; }
            public DateTime? ReturnDate { get; set; }

            // 🛠️ Maintenance & Readiness
            public int CurrentOdometer { get; set; }
            public DateTime? LastServiceDate { get; set; }
            public DateTime? NextServiceDue { get; set; }
            public int MaintenanceId { get; set; }
            public bool IsOperational { get; set; }

            // 🧾 Equipment & Configuration
            public bool HasBodyCamDock { get; set; }
            public bool HasInCarCamera { get; set; }
            public bool HasRifleMount { get; set; }
            public bool HasK9Cage { get; set; }
            public string RadioId { get; set; }
            public string MDTSerial { get; set; } // Mobile Data Terminal

            
            public DateTime? DecommissionedDate { get; set; }
            public string DecommissionReason { get; set; }

            public string Notes { get; set; }
            public string WarrantyInfo { get; set; }
            public string InsurancePolicyNumber { get; set; }
            public DateTime? InsuranceExpiryDate { get; set; }
            public string GPSUnitId { get; set; }
            public string CostCenterCode { get; set; }
            public string VendorInfo { get; set; }
            public string ReplacementSchedule { get; set; }
            public int? VehicleGroupId { get; set; }
            public string DigitalAccessLog { get; set; }
            public bool HasEmergencyLights { get; set; }
            public bool HasSiren { get; set; }
            public bool HasPushBumper { get; set; }
            public string CustomDecals { get; set; }
            public string InteriorConfiguration { get; set; }
            public string ExteriorConfiguration { get; set; }
            public string SpecializedEquipment { get; set; }
            public string CommunicationSystems { get; set; }
            public string SafetyFeatures { get; set; }
            public string PerformanceUpgrades { get; set; }


            // 🔗 Navigation Properties
            public ICollection<GearIssued> GearStored { get; set; }

            public string RecordCreatedBy { get; set; }
            public DateTime RecordCreatedDate { get; set; }
            public string RecordUpdatedBy { get; set; }
            public DateTime RecordUpdatedDate { get; set; }
        }
    }
}
