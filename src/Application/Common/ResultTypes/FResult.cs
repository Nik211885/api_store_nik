using Application.Common.ResultType;

namespace Application.Common.ResultTypes
{
    public static class FResult
    {
        public static Result Success()
        {
            return new Result(true);
        }
        public static Result Failure(params ResultError[] errors)
        {
            return new Result(false, errors);
        }
    }
}
