using Application.Common.ResultTypes;
using Application.Interface;
using Application.Reactions.Commands;
using MediatR;

namespace Application.Reactions.Handlers
{
    public class DeleteReactionCommandHandler : MediatR.IRequestHandler<DeleteReactionCommand, Result>
    {
        private readonly IStoreNikDbContext _dbContext;
        private readonly ISender _sender;
        public DeleteReactionCommandHandler(IStoreNikDbContext dbContext, ISender sender)
        {
            _dbContext = dbContext;
            _sender = sender;
        }
        public async Task<Result> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
