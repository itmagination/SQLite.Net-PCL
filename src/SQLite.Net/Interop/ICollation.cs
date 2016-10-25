namespace SQLite.Net.Interop
{
    public interface ICollation
    {
        string Name { get; }
        int Compare(byte[] leftTextBytes, byte[] rightTextBytes);
    }
}