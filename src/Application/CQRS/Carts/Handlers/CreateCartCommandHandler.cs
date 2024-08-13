using Application.Common.ResultTypes;
using Application.CQRS.Carts.Commands;
using Application.CQRS.Carts.Queries;
using Application.Interface;
using Application.Mappings;
using ApplicationCore.Entities.Order;
using MediatR;

namespace Application.CQRS.Carts.Handlers
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public CreateCartCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cartNotYetCheckOut = _sender.Send(new GetCartByUserQuery(request.UserId), cancellationToken);
            if (cartNotYetCheckOut is not null)
            {
                return FResult.Failure("Can't create cart because you can have one cart");
            }
            //
            var cart = Mapping<CreateCartCommand, Cart>.CreateMap().Map<Cart>(request);
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
