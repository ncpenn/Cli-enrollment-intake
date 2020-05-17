using CsvHelper.Configuration;
using EnrollmentIntake.Models;

namespace EnrollmentIntake.Interfaces
{
    public interface IFileHandler<TModel, TModelMapper> where TModelMapper : ClassMap<TModel>
    {
        RecordResults<TModel> ExtractRecordsFromFile(string filePath);

        bool IsValidFile(string filePath, string fileExtention);
    }
}
