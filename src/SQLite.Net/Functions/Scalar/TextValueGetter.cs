namespace SQLite.Net.Functions.Scalar
{
    public interface ITextValueGetter : IValueGetter
    {
        string Text { get; }
    }

    public class TextValueGetter : ITextValueGetter
    {
        public object Value { get; set; } = string.Empty;
        public string Text { get { return (string)Value; } }
    }
}