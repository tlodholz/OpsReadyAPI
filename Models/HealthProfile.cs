namespace OpsReady.Models
{
    public class HealthProfile
    {
        public int HealthProfileId { get; set; }
        public int UserId { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty;
        public string MedicalConditions { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactRelationship { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string PrimaryPhysicianName { get; set; } = string.Empty;
        public string PrimaryPhysicianPhone { get; set; } = string.Empty;
        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsurancePolicyNumber { get; set; } = string.Empty;
        public string RecentInjuries { get; set; } = string.Empty;
        public DateTime LastPhysicalExamDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
