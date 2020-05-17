using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnrollmentIntake.Interfaces;
using EnrollmentIntake.Models;

namespace EnrollmentIntake
{
    public class RecordProcessor<TUnprocessed, TProcessed> where TProcessed : TUnprocessed, IProcessedRecord, new()
    {
        private readonly PropertyInfo[] propertiesOfProcessed = typeof(TProcessed).GetProperties();
        private readonly List<PropertyInfo> propertiesOfUnprocessed = typeof(TUnprocessed).GetProperties().ToList();

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

            propertiesOfUnprocessed.ForEach(propInfo =>
            {
                this.propertiesOfProcessed
                   .First(p => p.Name.Equals(propInfo.Name))
                   .SetValue(processed, propInfo.GetValue(record));
            });

            RecordReceivedEvent?.Invoke(processed);

            return processed;
        }
    }
}
