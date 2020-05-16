namespace EnrollmentIntake.Models
{
    public sealed class ProcessedEnrollmentRecord
    {
        public EnrollmentStatus EnrollmentStatus { get; }

        public EnrollmentRecord EnrollmentRecord  { get; }

        public ProcessedEnrollmentRecord(EnrollmentRecord record, EnrollmentStatus status)
        {
            this.EnrollmentRecord = record;
            this.EnrollmentStatus = status;
        }
    }
}
