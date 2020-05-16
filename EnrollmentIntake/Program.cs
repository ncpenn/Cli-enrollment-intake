using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnrollmentIntake.Models;

namespace EnrollmentIntake.CsvReader
{
    class Program
    {
        private static IEnumerable<ProcessedEnrollmentRecord>  _processedRecords;

        static void Main(string[] args)
        {
            var csvPath = args[0];
            var recordHandler = RecordHandler.Create();
            recordHandler.RecordReceivedEvent += PublishToConsole;

            if (!FileHandler.IsValidFile(csvPath))
            {
                Console.WriteLine($"'{csvPath}' is not a valid file.");
            }

            var recordsResult = FileHandler.ExtractRecordsFromFile(csvPath);

            if (recordsResult.HasError)
            {
                Console.WriteLine("A record in the file failed validation. Processing has stopped.");
            }
            else
            {
                _processedRecords = recordsResult.EnrollmentRecords
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
