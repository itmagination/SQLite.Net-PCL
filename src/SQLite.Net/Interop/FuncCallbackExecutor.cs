using System;
using SQLite.Net.Functions.Scalar;

namespace SQLite.Net.Interop
{
    public class FuncCallbackExecutor
    {
        private readonly IScalarFunction _scalarFunction;
        private readonly Func<IntPtr, string> _textNativeGetter;
        private readonly Action<IntPtr, int> _intNativeResultSetter;

        public FuncCallbackExecutor(IScalarFunction scalarFunction, Func<IntPtr, string> textNativeGetter, Action<IntPtr, int> intNativeResultSetter)
        {
            _scalarFunction = scalarFunction;
            _textNativeGetter = textNativeGetter;
            _intNativeResultSetter = intNativeResultSetter;
        }

        public void Execute(IntPtr context, int valueIndex, IntPtr[] values)
        {
            for (var i = 0; i < values.Length; ++i)
            {
                var valueGetter = _scalarFunction.ValueGetters[i];
                if (valueGetter.Value is string)
                {
                    var ansiString = _textNativeGetter(values[i]);
                    valueGetter.Value = ansiString;
                }
            }

            var sqliteFunctionResult = _scalarFunction.GetResult();

            if (sqliteFunctionResult is int)
            {
                _intNativeResultSetter(context, (int)sqliteFunctionResult);
            }
        }
    }
}
