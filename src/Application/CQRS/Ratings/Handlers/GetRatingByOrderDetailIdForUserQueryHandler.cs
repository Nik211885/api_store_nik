using Application.Common.ResultTypes;
using Application.CQRS.OrderDetails.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using ApplicationCore.Entities.Order;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    public class GetRatingByOrderDetailIdForUserQueryHandler
        : IRequestHandler<GetRatingByOrderDetailIdForUserQuery, RatingReponse?>
    {
        private readonly ISender _sender;
        public GetRatingByOrderDetailIdForUserQueryHandler(ISender sender)
        {
            _sender = sender;
        }
        public async Task<RatingReponse?> Handle(GetRatingByOrderDetailIdForUserQuery request, CancellationToken cancellationToken)
        {
            // Check order detail has in user
            await _sender.Send(new GetOrderDetailByIdCheckOutOffUserQuery(request.UserId, request.OrderDetailId, true, false), cancellationToken);
            // Get Rating reponse
            var ratingResult = await _sender.Send(new GetRatingByOrderDetailIdQuery(request.OrderDetailId, true,request.UserId), cancellationToken);
            if(ratingResult.AttachedIsSuccess is null) return null;
            return (RatingReponse)ratingResult.AttachedIsSuccess;
        }
    }
}
