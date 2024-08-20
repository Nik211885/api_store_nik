using Application.CQRS.Products.Queries;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class ProductNameTypeReponse
    {
        public string Id { get; private set; } = null!;
        public string NameType { get; private set; } = null!;   
        public IEnumerable<ProductValueTypeReponse> ValueTypes { get; private set; } = null!;
        public async Task Join(ISender sender)
        {
            ValueTypes = await sender.Send(new GetProductValueTypeByNameTypeQuery(Id));
        }
    }
}
