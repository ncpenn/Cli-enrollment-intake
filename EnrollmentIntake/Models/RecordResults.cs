using System.Collections.Generic;

namespace EnrollmentIntake.Models
{
    public class RecordResults<TUnprocessedRecord>
    {
        public RecordResults(IEnumerable<TUnprocessedRecord> records, string errorMessage = null)
        {
            this.Records = records as IReadOnlyCollection<TUnprocessedRecord>;
            ErrorMessage = errorMessage;
        }

        public IReadOnlyCollection<TUnprocessedRecord> Records { get; }

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
