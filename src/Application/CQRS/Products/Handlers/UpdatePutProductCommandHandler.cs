using Application.Common.ResultTypes;
using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.DTOs.Request;
using Application.Interface;
using ApplicationCore.Entities.Products;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class UpdatePutProductCommandHandler : IRequestHandler<UpdatePutProductCommand, IResult>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public UpdatePutProductCommandHandler(IStoreNikDbContext dbContext, ISender sender, IMapper mapper)
        {
            _mapper = mapper;
            _sender = sender;
            _dbContext = dbContext;
        }
        public async Task<IResult> Handle(UpdatePutProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(new GetProductByIdQuery(request.Id),cancellationToken);
            if (product is not null)
            {
                await _sender.Send(new IsProductForUserQuery(request.UserId, request.Id), cancellationToken);
                product = _mapper.Map(request.Product, product);
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return FResult.Success();
            }
            return FResult.NotFound(request.Id,nameof(Product));
        }
    }
}
