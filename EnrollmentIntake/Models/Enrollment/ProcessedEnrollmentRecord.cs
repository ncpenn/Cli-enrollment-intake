using EnrollmentIntake.Interfaces;

namespace EnrollmentIntake.Models.Enrollment
{
    public sealed class ProcessedEnrollmentRecord : EnrollmentRecord, IProcessedRecord
    {
        public Status RecordStatus { get; set; }
    }
}
