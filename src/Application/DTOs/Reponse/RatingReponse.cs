using Application.CQRS.Reactions.Queries;
using ApplicationCore.Entities.Ratings;
using AutoMapper;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class RatingReponse
    {

        public string Id { get; init; } = null!;
        public int Start { get; init; }
        public string? CommentRating { get; init; }
        public DateTime DateRating { get; init; }
        public string? Reaction { get; private set; }
        public long LikeCount { get; private set; }
        public long UnLikeCount {  get; private set; }
        public async Task Join(ISender sender, string? userId = null)
        {
            var result = await sender.Send(new GetLikeAndUnLikeForRatingQuery(Id));
            LikeCount = result[0];
            UnLikeCount = result[1];
            if(userId is not null)
            {
                var reaction = await sender.Send(new GetReactionByUserForRatingQuery(userId, Id));
                if(reaction is not null)
                {
                    Reaction = reaction.Like ? "Like" : "Unlike";
                }
            }
        }
    };
    public class MappingRating : Profile
    {
        public MappingRating()
        {
            CreateMap<Rating, RatingReponse>();
        }
    }
}
