namespace OpsReady.Models
{
    public class TrainingEvent
    {
        public int TrainingEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RecordedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Instructor { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public bool CertificationIssued { get; set; }
        public string CertificationIssuedBy { get; set; }
        public DateTime CertificationExpiryDate { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public string RecordUpdatedBy { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
    }
}
