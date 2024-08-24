using Application.CQRS.Products.Queries;
using MediatR;

namespace Application.DTOs.Reponse
{
    public class ProductNameTypeReponse
    {
        public string Id { get; init; } = null!;
        public string NameType { get; init; } = null!;
        public IEnumerable<ProductValueTypeReponse> ValueTypes { get; private set; } = [];
        public async Task Join(ISender sender, Option? option = null)
        {
            ValueTypes = await sender.Send(new GetProductValueTypeByNameTypeQuery(Id, option));
            foreach(var item in ValueTypes)
            {
                await item.Join(sender, option);
            }
        }
    }
}
