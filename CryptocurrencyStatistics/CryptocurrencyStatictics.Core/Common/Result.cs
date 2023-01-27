using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatictics.Core.Common
{
    public class Result<TValue>
    {
        public Error Error { get; set; }
        public TValue Value { get; set; }

        public static Result<TValue> Success(TValue value) =>
            new Result<TValue> { Value = value };

        public static Result<TValue> Fail(Error error) =>
            new Result<TValue> { Error = error };

        public static Result<TValue> Fail(string errorDescription) =>
            new Result<TValue> { Error = new Error {  Description = errorDescription } };

        public bool IsSuccess => Error == null;
    }

    public class VoidResult
    {
    }

    public class Error
    {
        public string Description { get; set; }
    }
}
