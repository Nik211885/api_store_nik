using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.Carts.Queries
{
    /// <summary>
    /// Default Get Cart for user has not yet checkout
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="IsCheckOut"></param>
    /// <param name="IsIncludeProductInCart">IsIncludeProductInCart is true If you want to use join and gender all about product in cart</param>
    public record GetCartByUserQuery(string UserId, bool IsCheckOut = false, bool IsIncludeProductInCart = false) : IRequest<Cart?>;
}
