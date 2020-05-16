using System.Collections.Generic;

namespace EnrollmentIntake.Models
{
    public class EnrollmentRecordResults
    {
        public EnrollmentRecordResults(IEnumerable<EnrollmentRecord> enrollmentRecords, string errorMessage = null)
        {
            EnrollmentRecords = enrollmentRecords as IReadOnlyCollection<EnrollmentRecord>;
            ErrorMessage = errorMessage;
        }

        public IReadOnlyCollection<EnrollmentRecord> EnrollmentRecords { get; }

        public string ErrorMessage { get; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(this.ErrorMessage);
            }
        }
    }
}
