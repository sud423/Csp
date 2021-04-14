using System.Collections.Generic;

namespace Csp.Result
{
    public class CspResult
    {
        public bool Succeed { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public CspResult(bool succeed, params string[] errors)
        {
            Succeed = succeed;
            Errors = errors;
        }

        public static CspResult Fail(params string[] errors)
        {
            return new CspResult(false, errors);
        }

        public static CspResult Ok()
        {
            return new CspResult(true, null);
        }

        public static CspResult<TValue> Ok<TValue>(TValue value)
        {
            return new CspResult<TValue>(value, true, null);
        }

        public static CspResult<TValue> Fail<TValue>(params string[] errors)
        {
            return new CspResult<TValue>(default(TValue), false, errors);
        }
    }
}
