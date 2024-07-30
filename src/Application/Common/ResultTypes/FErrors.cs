using Application.Common.ResultType;

namespace Application.Common.ResultTypes
{
    public static class FErrors
    {
        public static ResultError NotFound(string id)
        {
            return new ResultError(nameof(NotFound), $"Don't found entity have {id}");
        }
    }
}
