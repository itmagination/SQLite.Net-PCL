using System.Text;

namespace SQLite.Net.Functions.Scalar
{
    public class ContainsScalarFunction : IScalarFunction
    {
        public const string Function_Name = "contains";

        public string Name { get; } = Function_Name;
        public IValueGetter[] ValueGetters { get; }

        ITextValueGetter _leftTextValueGetter = new TextValueGetter();
        ITextValueGetter _rightTextValueGetter = new TextValueGetter();

        public ContainsScalarFunction()
        {
            ValueGetters = new IValueGetter[] { _leftTextValueGetter, _rightTextValueGetter };
        }

        public object GetResult()
        {
            var leftValue = _leftTextValueGetter.Text;
            var rightValue = _rightTextValueGetter.Text;

            if (leftValue == null || rightValue == null)
                return 0;

            var leftValueUtf8 = GetUTF8String(leftValue);
            var rightValueUtf8 = GetUTF8String(rightValue);

            var result = string.IsNullOrEmpty(rightValueUtf8) == false && leftValueUtf8.ToLower().Contains(rightValueUtf8.ToLower()) ? 1 : 0;

            return result;
        }

        private string GetUTF8String(string text)
        {
            var windowsBytes = Encoding.GetEncoding("Windows-1252").GetBytes(text);
            var uft8string = Encoding.UTF8.GetString(windowsBytes, 0, windowsBytes.Length);
            return uft8string;
        }
    }
}
