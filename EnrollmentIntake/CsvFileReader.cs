using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class CsvFileReader<TUnprocessedRecord, TRecordClassMap> : IFileHandler<TUnprocessedRecord, TRecordClassMap> where TRecordClassMap : ClassMap<TUnprocessedRecord>
    {
        public RecordResults<TUnprocessedRecord> ExtractRecordsFromFile(string filePath)
        {
            using (var fileHanlde = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(fileHanlde, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<TRecordClassMap>();

                try
                {
                    return new RecordResults<TUnprocessedRecord>(csv.GetRecords<TUnprocessedRecord>().ToList());
                }
                catch (Exception ex)
                {
                    return new RecordResults<TUnprocessedRecord>(null, ex.Message);
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
