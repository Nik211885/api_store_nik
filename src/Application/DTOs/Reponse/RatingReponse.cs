using Application.CQRS.Reactions.Queries;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class RatingReponse
    {

        public string Id { get; private set; } = null!;
        public int Start { get; private set; }
        public string? CommentRating { get; private set; }
        public DateTime DateRating { get; private set; }
        public long LikeCount { get; private set; }
        public long UnLikeCount {  get; private set; }
        public async Task Join(ISender sender)
        {
            var result = await sender.Send(new GetLikeAndUnLikeForRatingQuery(Id));
            LikeCount = result[0];
            UnLikeCount = result[1];
        }
    };
}
