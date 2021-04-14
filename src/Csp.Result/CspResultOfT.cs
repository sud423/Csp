namespace Csp.Result
{
    public class CspResult<TValue> : CspResult
    {
        public TValue Value { get; set; }

        public CspResult(TValue value, bool success, params string[] errors)
            : base(success, errors)
        {
            Value = value;
        }
    }
}
