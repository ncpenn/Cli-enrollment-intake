using System.Collections.Generic;

namespace EnrollmentIntake.Models
{
    public class RecordResults<T>
    {
        public RecordResults(IEnumerable<T> records, string errorMessage = null)
        {
            this.Records = records as IReadOnlyCollection<T>;
            ErrorMessage = errorMessage;
        }

        public IReadOnlyCollection<T> Records { get; }

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
