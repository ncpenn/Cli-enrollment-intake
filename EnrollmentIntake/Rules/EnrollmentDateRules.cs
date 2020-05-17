using System;
using EnrollmentIntake.Interfaces;

namespace EnrollmentIntake.Rules
{
    public class EnrollmentDateRules : IRules
    {
        private DateTime dob;
        private DateTime effectiveDate;
        private Func<DateTime, bool> dobFunc = (dob) => dob.AddYears(18) < DateTime.Now;
        private Func<DateTime, bool> effectiveDateFunc = (e) => DateTime.Now.AddDays(30) >= e;

        public bool Do()
        {
            return dobFunc(dob) && effectiveDateFunc(effectiveDate);
        }

        public EnrollmentDateRules WithDOB(DateTime dateTime)
        {
            this.dob = dateTime;
            return this;
        }

        public EnrollmentDateRules WithEffectiveDate(DateTime dateTime)
        {
            this.effectiveDate = dateTime;
            return this;
        }
    }
}
