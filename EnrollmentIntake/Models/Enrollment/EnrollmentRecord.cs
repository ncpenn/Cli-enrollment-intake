using System;

namespace EnrollmentIntake.Models.Enrollment
{
    public class EnrollmentRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string PlanType { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
