using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsReady.Models
{
    [Table("OpsReady_TrainingEvent")]
    public class TrainingEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TrainingType { get; set; } = string.Empty; //(Classroom, Simulation, Field Exercise)
        public string TrainingCategory { get; set; } = string.Empty;
        public string TrainingSubCategory { get; set; } = string.Empty;
        public string TrainingTier { get; set; } = string.Empty;
        public string SkillLevel { get; set; } = string.Empty;
        public string SkillArea { get; set; } = string.Empty;
        public string AssessmentCriteria { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public string Objectives { get; set; } = string.Empty;
        public string MaterialsProvided { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EnrollmentDeadline { get; set; }
        public string Location { get; set; } = string.Empty; //(Academy, Online, Field)
        public string Provider { get; set; } = string.Empty; //(Academy, External Vendor)
        public string Instructor { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public bool CertificationIssued { get; set; }
        public string CertificationIssuedBy { get; set; } = string.Empty;
        public DateTime CertificationExpiryDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string MandatedBy { get; set; } = string.Empty;
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
