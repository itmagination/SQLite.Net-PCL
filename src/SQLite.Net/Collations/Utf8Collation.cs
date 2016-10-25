using System;
using System.Text;
using SQLite.Net.Interop;

namespace SQLite.Net.Collations
{
    public class Utf8Collation : ICollation
    {
        public const string Collation_Name = "utf8";

        public string Name { get; } = Collation_Name;

        public int Compare(byte[] leftTextBytes, byte[] rightTextBytes)
        {
            var leftText = Encoding.UTF8.GetString(leftTextBytes, 0, leftTextBytes.Length);
            var rightText = Encoding.UTF8.GetString(rightTextBytes, 0, rightTextBytes.Length);

            return string.Compare(leftText, rightText, StringComparison.CurrentCulture);
        }
    }
}
