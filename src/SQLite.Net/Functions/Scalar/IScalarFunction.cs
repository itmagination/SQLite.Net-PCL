namespace SQLite.Net.Functions.Scalar
{
    public interface IScalarFunction
    {
        string Name { get; }
        IValueGetter[] ValueGetters { get; }
        object GetResult();
    }
}