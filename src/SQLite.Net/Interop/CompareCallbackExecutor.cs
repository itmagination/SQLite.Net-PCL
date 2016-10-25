using System;

namespace SQLite.Net.Interop
{
    public class CompareCallbackExecutor
    {
        private readonly ICollation _collation;
        private readonly Func<IntPtr, int, byte[]> _compareCallbackStringBytesGetter;

        public CompareCallbackExecutor(ICollation collation, Func<IntPtr, int, byte[]> compareCallbackStringBytesGetter)
        {
            _collation = collation;
            _compareCallbackStringBytesGetter = compareCallbackStringBytesGetter;
        }

        public int Execute(IntPtr pvUser, int len1, IntPtr pv1, int len2, IntPtr pv2)
        {
            var leftParamBytes = _compareCallbackStringBytesGetter(pv1, len1);
            var rightParamBytes = _compareCallbackStringBytesGetter(pv2, len2);
            return _collation.Compare(leftParamBytes, rightParamBytes);
        }
    }
}