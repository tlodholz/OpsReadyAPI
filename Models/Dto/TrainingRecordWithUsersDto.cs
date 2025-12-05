using System;

namespace OpsReadyAPI.Models.Dto
{
    public class TrainingRecordWithUsersDto
    {
        // All TrainingRecord fields
        public int Id { get; set; }
        public int TrainingAssignmentId { get; set; }
        public string AssignedBy { get; set; } = string.Empty;
        public string Attendance { get; set; } = string.Empty;
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
        public string RecordCreatedBy { get; set; } = string.Empty;
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; } = string.Empty;
        public DateTime RecordUpdatedDate { get; set; }

        // Minimal user/profile information
        public UserProfileDto? User { get; set; }

        public class UserProfileDto
        {
            public int Id { get; set; }                 // profile PK
            public int? UserId { get; set; }           // FK to user
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string EmailAddress { get; set; } = string.Empty;
            public string BadgeNumber { get; set; } = string.Empty;
        }
    }
}
