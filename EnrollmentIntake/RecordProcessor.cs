using System.Linq;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class RecordProcessor<TUnprocessed, TProcessed> where TProcessed : TUnprocessed, IProcessedRecord, new()
    {
        private readonly IRules<TUnprocessed> rules;

        public delegate void RecordProcessed(TProcessed record);
        public event RecordProcessed RecordReceivedEvent;

        public RecordProcessor(IRules<TUnprocessed> rules)
        {
            this.rules = rules;
        }

        public TProcessed ProcessRecord(TUnprocessed record)
        {
            var status = rules.Do(record)
                ? Status.Accepted
                : Status.Rejected;

            var processed = new TProcessed
            {
                RecordStatus = status
            };

            var propertiesOfProcessed = typeof(TProcessed).GetProperties();

            foreach (var propInfo in typeof(TUnprocessed).GetProperties())
            {
                propertiesOfProcessed
                    .First(p => p.Name.Equals(propInfo.Name))
                    .SetValue(processed, propInfo.GetValue(record));
            }

            RecordReceivedEvent?.Invoke(processed);

            return processed;
        }
    }
}
