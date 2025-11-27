using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsReady.Models
{
    [Table("OpsReady_UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PreferredName { get; set; }
        public string? BadgeNumber { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? Location { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Skills { get; set; }
        public string? Status { get; set; } // Active, Suspended, Retired, etc.
        public string? Rank { get; set; }
        public string? Bio { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfHire { get; set; }
        public string? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public string? ShiftSchedule { get; set; }
        public string? SupervisorBadgeNumber { get; set; }
        public string? StationLocation { get; set; }
        public string? LegalRestrictions { get; set; }
        public string? ExemptionsGranted { get; set; }
        public string? DisciplinaryActions { get; set; }
        public string? UseOfForceClearance { get; set; }
        public string? LanguageFluency { get; set; }
        public string? SpecialClearances { get; set; }
        public bool? IsActiveDuty { get; set; }
        public string? CommandingOfficerBadgeNumber { get; set; }
        public string? Notes { get; set; }
        public string? RecordCreatedBy { get; set; }
        public DateTime? RecordCreatedDate { get; set; }
        public string? RecordUpdatedBy { get; set; }
        public DateTime? RecordUpdatedDate { get; set; }
    }
}
