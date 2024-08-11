namespace Application.Interface
{
    public interface IResult
    {
        string? StatusCode { get; set; }
        bool Success { get; set; }
        IEnumerable<string>? Errors { get; set; }
    }
}
