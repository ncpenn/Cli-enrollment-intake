using System;
using System.Globalization;
using System.IO;
using System.Linq;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public static class FileHandler
    {
        public static EnrollmentRecordResults ExtractRecordsFromFile(string csvPath)
        {
            using (var fileHanlde = new StreamReader(csvPath))
            using (var csv = new CsvHelper.CsvReader(fileHanlde, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<EnrollmentRecordMap>();

                try
                {
                    return new EnrollmentRecordResults(csv.GetRecords<EnrollmentRecord>().ToList());
                }
                catch (Exception ex)
                {
                    return new EnrollmentRecordResults(null, ex.Message);
                }
            }
        }

        public static bool IsValidFile(string csvPath)
        {
            return csvPath != null &&
                File.Exists(csvPath) &&
                Path.GetExtension(csvPath)
                    .Equals(".csv", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
