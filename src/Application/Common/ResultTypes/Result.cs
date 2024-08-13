using Application.Interface;

namespace Application.Common.ResultTypes
{
    public class Result: IResult
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public string? StatusCode { get; set; }
        public dynamic? AttachedIsSuccess { get; set; }

        public bool Failure() => !Success;
    }
}
