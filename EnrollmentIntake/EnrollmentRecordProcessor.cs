using EnrollmentIntake.Models;
using EnrollmentIntake.Rules;

namespace EnrollmentIntake
{
    public class EnrollmentRecordProcessor
    {
        private readonly EnrollmentDateRules rules;

        public delegate void RecordProcessed(ProcessedEnrollmentRecord record);
        public event RecordProcessed RecordReceivedEvent;

        public static EnrollmentRecordProcessor Create(EnrollmentDateRules enrollmentDateRules)
        {
            return new EnrollmentRecordProcessor(enrollmentDateRules);
        }

        private EnrollmentRecordProcessor(EnrollmentDateRules rules)
        {
            this.rules = rules;
        }

        public ProcessedEnrollmentRecord ProcessRecord(EnrollmentRecord record)
        {
            var enrollmentStatus = rules
                                    .WithDOB(record.DOB)
                                    .WithEffectiveDate(record.EffectiveDate)
                                    .Do()
                ? EnrollmentStatus.Accepted
                : EnrollmentStatus.Rejected;

            var processedRecord = new ProcessedEnrollmentRecord(record, enrollmentStatus);

            RecordReceivedEvent?.Invoke(processedRecord);

            return processedRecord;
        }
    }
}
