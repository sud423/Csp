namespace Csp
{
    public class Result
    {
        public bool Succeed { get; set; }


        public string Error { get; set; }

        public Result() { }

        protected Result(bool succeed, string error)
        {
            Succeed = succeed;
            Error = error;
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }

        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>(value, true, null);
        }

        public static Result<TValue> Fail<TValue>(string error)
        {
            return new Result<TValue>(default(TValue), false, error);
        }
    }
}
