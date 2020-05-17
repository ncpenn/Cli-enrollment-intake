using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models.Enrollment;
using EnrollmentIntake.Rules;

namespace EnrollmentIntake.CsvReader
{
    class Program
    {
        private const string CSV_EXTENTION = ".csv";

        private static IEnumerable<ProcessedEnrollmentRecord> processedRecords;
        private static readonly IFileHandler<EnrollmentRecord, EnrollmentRecordMap> fileHandler;
        private static readonly RecordProcessor<EnrollmentRecord, ProcessedEnrollmentRecord> recordHandler;

        static Program()
        {
            IRules<EnrollmentRecord> rules = new EnrollmentDateRules();
            fileHandler = new CsvFileReader<EnrollmentRecord, EnrollmentRecordMap>();
            recordHandler = new RecordProcessor<EnrollmentRecord, ProcessedEnrollmentRecord>(rules);
            recordHandler.RecordReceivedEvent += PublishToConsole;
        }

        static void Main(string[] args)
        {
            var csvPath = args[0];

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
            sb.Append($"{processesRecord.RecordStatus},");
            sb.Append($"{processesRecord.FirstName},");
            sb.Append($"{processesRecord.LastName},");
            sb.Append($"{processesRecord.DOB.ToShortDateString()},");
            sb.Append($"{processesRecord.PlanType},");
            sb.Append($"{processesRecord.EffectiveDate.ToShortDateString()}");

            Console.WriteLine(sb.ToString());
        }
    }
}
