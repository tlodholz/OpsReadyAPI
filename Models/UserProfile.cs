using System.ComponentModel.DataAnnotations.Schema;

namespace OpsReady.Models
{

    [Table("OpsReady_UserProfile")]
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PreferredName { get; set; } = string.Empty;
        public string BadgeNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; //Active, Suspended, Retired, etc.
        public string Rank { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; }= string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfHire { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Ethnicity { get; set; } = string.Empty;
        public string ShiftSchedule { get; set; } = string.Empty;
        public string SupervisorBadgeNumber { get; set; } = string.Empty;
        public string StationLocation { get; set; } = string.Empty;
        public string LegalRestrictions { get; set; } = string.Empty;
        public string ExemptionsGranted { get; set; } = string.Empty;
        public string DisciplinaryActions { get; set; } = string.Empty;
        public string UseOfForceClearance { get; set; } = string.Empty;
        public string LanguageFluency { get; set; } = string.Empty;
        public string SpecialClearances { get; set; } = string.Empty;
        public bool IsActiveDuty { get; set; }
        public string CommandingOfficerBadgeNumber { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
