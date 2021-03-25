namespace Csp
{
    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }

        public Result() { }

        protected internal Result(TValue value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }
}
