using Application.Common.ResultTypes;
using Application.CQRS.OrderDetails.Queries;
using Application.CQRS.Ratings.Commands;
using Application.CQRS.Ratings.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Ratings;
using ApplicationCore.ValueObject;
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Ratings.Handlers
{
    //User just have one rating and just create don't delete and update rating 
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public CreateRatingCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            //Check order detail has in user and has check out
            var countOrderDetail = await _sender.Send(new GetOrderDetailCheckOutOffUserQuery(request.UserId, request.Rating.OrderDetailId, IsCheckOut: true, IsOption: false), cancellationToken);
            Guard.Against.Expression(x=>x!=1, (int)(countOrderDetail.AttachedIsSuccess), nameof(OrderDetail), VariableException.BadRequest);
            //Check order detail has rating relationship stay here is one to one one order detail has one rating
            var countRating = await _sender.Send(new GetRatingByOrderDetailIdQuery(request.Rating.OrderDetailId, IsOption: false),cancellationToken);
            Guard.Against.Expression(x => x != 0, (int)(countRating.AttachedIsSuccess), "You can have one rating for one order has check out");
            var rating = new Rating(request.Rating.OrderDetailId, request.Rating.Start, request.Rating.CommentRating);
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
