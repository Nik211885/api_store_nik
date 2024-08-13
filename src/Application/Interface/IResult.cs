namespace Application.Interface
{
    public interface IResult
    {
        string? StatusCode { get; set; }
        bool Success { get; set; }
        IEnumerable<string>? Errors { get; set; }
        /// <summary>
        ///     If you want to attached som thing else if result is success
        /// </summary>
        dynamic? AttachedIsSuccess { get; set; }
        bool Failure();
    }
}
