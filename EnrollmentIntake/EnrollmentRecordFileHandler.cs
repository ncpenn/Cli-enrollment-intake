using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class EnrollmentRecordFileHandler<T, U> : IFileHandler<T, U> where U : ClassMap<T>
    {
        public RecordResults<T> ExtractRecordsFromFile(string filePath)
        {
            using (var fileHanlde = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(fileHanlde, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<U>();

                try
                {
                    return new RecordResults<T>(csv.GetRecords<T>().ToList());
                }
                catch (Exception ex)
                {
                    return new RecordResults<T>(null, ex.Message);
                }
            }
        }

        public bool IsValidFile(string filePath, string fileExtention)
        {
            return filePath != null &&
                File.Exists(filePath) &&
                Path.GetExtension(filePath)
                    .Equals(fileExtention, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
