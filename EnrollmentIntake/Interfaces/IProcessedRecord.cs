using EnrollmentIntake.Models;

namespace EnrollmentIntake.Interfaces
{
    public interface IProcessedRecord
    {
        Status RecordStatus { get; set; }
    }
}
