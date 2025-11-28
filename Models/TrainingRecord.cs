using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsReady.Models
{
    [Table("OpsReady_TrainingRecord")]
    public class TrainingRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TrainingEventId { get; set; }
        public int UserId { get; set; }
        public string AssignedBy { get; set; } = string.Empty;
        public string Attendance { get; set; } = string.Empty;// (Attended, No-Show, Cancelled,Absent, Excused, In Progress)
        public string Status { get; set; } = string.Empty;
        public string TrainingOutcome { get; set; } = string.Empty;
        public string Score { get; set; } = string.Empty;
        public string CertificationNumber { get; set; } = string.Empty;
        public string SkillLevelAchieved { get; set; } = string.Empty;
        public string ProficiencyLevel { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int HoursCompleted { get; set; } = 0;
        public bool Completed { get; set; }
        public string Strengths { get; set; } = string.Empty;
        public string AreasForImprovement { get; set; } = string.Empty;
        public bool FollowUpRequired { get; set; }
        public int EvaluatorId { get; set; }
        public string Evaluator { get; set; } = string.Empty;
        public string EvaluationComments { get; set; } = string.Empty;
        public string EvaluationType { get; set; } = string.Empty;
        public string OfficialComments { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public DateTime EvaluationDate { get; set; }
        public DateTime FollowUpDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public DateTime CertificationIssuedDate { get; set; }
        public DateTime CertificationExpiryDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
