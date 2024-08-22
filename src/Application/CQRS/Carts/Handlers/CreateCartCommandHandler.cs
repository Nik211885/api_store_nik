using Application.Common.ResultTypes;
using Application.CQRS.Carts.Commands;
using Application.CQRS.Carts.Queries;
using Application.Interface;
using ApplicationCore.Entities.Order;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Carts.Handlers
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public CreateCartCommandHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper; 
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<IResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cartNotYetCheckOut = await _sender.Send(new GetCartHasNotCheckOutByUserQuery(request.UserId), cancellationToken);
            if (cartNotYetCheckOut is not null)
            {
                return FResult.Failure("Can't create cart because you can have one cart");
            }
            //
            var cart = new Cart(request.UserId);
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return FResult.Success();
        }
    }
}
