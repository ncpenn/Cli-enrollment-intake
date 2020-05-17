using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;
using EnrollmentIntake.Rules;

namespace EnrollmentIntake.CsvReader
{
    class Program
    {
        private const string CSV_EXTENTION = ".csv";

        private static IEnumerable<ProcessedEnrollmentRecord> processedRecords;
        private static readonly IFileHandler<EnrollmentRecord, EnrollmentRecordMap> fileHandler;
        private static readonly EnrollmentRecordProcessor recordHandler;

        static Program()
        {
            fileHandler = new EnrollmentRecordFileHandler<EnrollmentRecord, EnrollmentRecordMap>();
            recordHandler = EnrollmentRecordProcessor.Create(new EnrollmentDateRules());
            recordHandler.RecordReceivedEvent += PublishToConsole;
        }

        static void Main(string[] args)
        {
            //var csvPath = args[0];
            var csvPath = "/Users/pennington/test.csv";

            if (!fileHandler.IsValidFile(csvPath, CSV_EXTENTION))
            {
                Console.WriteLine($"'{csvPath}' is not a valid file.");
            }

            var recordsResult = fileHandler.ExtractRecordsFromFile(csvPath);

            if (recordsResult.HasError)
            {
                Console.WriteLine("A record in the file failed validation. Processing has stopped.");
            }
            else
            {
                processedRecords = recordsResult.Records
                    .Select(recordHandler.ProcessRecord)
                    .ToList();
            }
        }

        private static void PublishToConsole(ProcessedEnrollmentRecord processesRecord)
        {
            var sb = new StringBuilder();
            sb.Append($"{processesRecord.EnrollmentStatus},");
            sb.Append($"{processesRecord.EnrollmentRecord.FirstName},");
            sb.Append($"{processesRecord.EnrollmentRecord.LastName},");
            sb.Append($"{processesRecord.EnrollmentRecord.DOB.ToShortDateString()},");
            sb.Append($"{processesRecord.EnrollmentRecord.PlanType},");
            sb.Append($"{processesRecord.EnrollmentRecord.EffectiveDate.ToShortDateString()}");

            Console.WriteLine(sb.ToString());
        }
    }
}
