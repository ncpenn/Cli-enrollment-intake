namespace EnrollmentIntake.Interfaces
{
    public interface IRules<TModel>
    {
        bool Do(TModel item);
    }
}
