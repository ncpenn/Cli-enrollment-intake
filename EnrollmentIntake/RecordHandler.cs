using System;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class RecordHandler
    {
        public delegate void RecordProcessed(ProcessedEnrollmentRecord record);
        public event RecordProcessed RecordReceivedEvent;

        public static RecordHandler Create()
        {
            return new RecordHandler();
        }

        private RecordHandler()
        {
        }

        public ProcessedEnrollmentRecord ProcessRecord(EnrollmentRecord record)
        {
            var applicantIsAtLeast18YearsOld = record.DOB.AddYears(18) < DateTime.Now;
            var effectiveIsWithIn30DaysOfNow = DateTime.Now.AddDays(30) >= record.EffectiveDate;

            var enrollmentStatus = applicantIsAtLeast18YearsOld && effectiveIsWithIn30DaysOfNow
                ? EnrollmentStatus.Accepted
                : EnrollmentStatus.Rejected;

            var processedRecord = new ProcessedEnrollmentRecord(record, enrollmentStatus);

            RecordReceivedEvent?.Invoke(processedRecord);

            return processedRecord;
        }
    }
}
