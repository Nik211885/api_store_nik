using MediatR;

namespace Application.CQRS.Carts.Queries
{
    /// <summary>
    ///     Just apply if you know carts is not check out and belong to for use
    /// </summary>
    /// <param name="CartId"></param>
    /// <param name="ProductId"></param>
    internal record GetQuantityCartNotCheckOutHasProductQuery(string CartId, string ProductId) : IRequest<int>;
}
