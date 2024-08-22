namespace Application.DTOs.Request
{
    public record CreateRatingViewModel(string OrderDetailId,
        int Start,
        string? CommentRating);
}
