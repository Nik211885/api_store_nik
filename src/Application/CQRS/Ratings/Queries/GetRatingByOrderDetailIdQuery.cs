using Application.Interface;
using MediatR;

namespace Application.CQRS.Ratings.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="OrderDetailId"></param>
    /// <param name="IsOption">True will attack object false is attack count object</param>
    public record GetRatingByOrderDetailIdQuery(string OrderDetailId, bool IsOption = true) : IRequest<IResult>;
}
