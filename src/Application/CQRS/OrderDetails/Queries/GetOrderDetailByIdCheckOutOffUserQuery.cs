using Application.Interface;
using MediatR;

namespace Application.CQRS.OrderDetails.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="OrderId"></param>
    /// <param name="IsCheckOut"></param>
    /// <param name="IsOption">Option true will attack object and false is count object</param>
    internal record GetOrderDetailByIdCheckOutOffUserQuery(string UserId, string OrderId, bool IsCheckOut = false, bool IsOption = true)
        : IRequest<IResult>;
}
