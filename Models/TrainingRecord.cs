namespace OpsReady.Models
{
    public class TrainingRecord
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int TrainingEventId { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Status { get; set; }
        public string TrainingOutcome { get; set; }
        public string Score { get; set; }
        public string ProficiencyLevel { get; set; }
        public string Notes { get; set; }
        public int HoursCompleted { get; set; }
        public bool Completed { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string Strengths { get; set; }
        public string AreasForImprovement { get; set; }
        public bool FollowUpRequired { get; set; }
        public DateTime FollowUpDate { get; set; }
        public int EvaluatorId { get; set; }
        public string EvaluatorBadgeNumber { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string EvaluationType { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
