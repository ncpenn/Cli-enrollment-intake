using System;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models.Enrollment;

namespace EnrollmentIntake.Rules
{
    public class EnrollmentDateRules : IRules<EnrollmentRecord>
    {
        private readonly Func<DateTime, bool> dobFunc = (dob) => dob.AddYears(18) < DateTime.Now;
        private readonly Func<DateTime, bool> effectiveDateFunc = (e) => DateTime.Now.AddDays(30) >= e;

        public bool Do(EnrollmentRecord item)
        {
            return dobFunc(item.DOB) && effectiveDateFunc(item.EffectiveDate);
        }
    }
}
