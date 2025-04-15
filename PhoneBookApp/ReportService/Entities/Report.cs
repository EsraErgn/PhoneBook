namespace ReportService.Entities
{
    public enum ReportStatus
    {
        Preparing = 0,
        Completed = 1
    }

    public class Report
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public ReportStatus Status { get; set; } = ReportStatus.Preparing;
        public string Location { get; set; }

        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
