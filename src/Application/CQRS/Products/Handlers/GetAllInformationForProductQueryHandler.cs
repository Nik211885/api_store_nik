using Application.CQRS.Products.Queries;
using Application.CQRS.Ratings.Queries;
using Application.DTOs.Reponse;
using ApplicationCore.Entities.Ratings;
using MediatR;

namespace Application.CQRS.Products.Handlers
{
    public class GetAllInformationForProductQueryHandler
        : IRequestHandler<GetAllInformationForProductQuery, ProductDTO?>
    {
        private ISender _sender;
        public GetAllInformationForProductQueryHandler(
            ISender sender
            )
        {
            _sender = sender;
        }
        public async Task<ProductDTO?> Handle(GetAllInformationForProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _sender.Send(new GetProductByIdQuery(request.ProductId),cancellationToken);
            if(product is null)
            {
                return null;
            }
            var statisticRating = await _sender.Send(new GetStatisticsRatingForProductQuery(request.ProductId),cancellationToken);
            var reponse = new ProductDTO(product, statisticRating);
            return reponse;
        }
    }
}
