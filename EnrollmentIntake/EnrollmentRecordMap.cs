using System;
using System.Globalization;
using CsvHelper.Configuration;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class EnrollmentRecordMap : ClassMap<EnrollmentRecord>
    {
        private static readonly CultureInfo _cultureInfo = new CultureInfo("en-US");

        public EnrollmentRecordMap()
        {
            Map(m => m.FirstName).Index(0).Validate(m => !string.IsNullOrWhiteSpace(m));
            Map(m => m.LastName).Index(1).Validate(m => !string.IsNullOrWhiteSpace(m));
            Map(m => m.DOB).Index(2).ConvertUsing(row => GetValidDate(row.GetField(2)));
            Map(m => m.PlanType).Index(3).Validate(m => IsAllowedPlanType(m));
            Map(m => m.EffectiveDate).Index(4).ConvertUsing(row => GetValidDate(row.GetField(4)));
        }

        private bool IsAllowedPlanType(string input)
        {
            return Enum.IsDefined(typeof(PlanType), input);
        }

        private DateTime GetValidDate(string input)
        {
            return DateTime.ParseExact(input.Trim(), "MMddyyyy", _cultureInfo);
        }
    }
}
