using Application.DTOs.Reponse;
using MediatR;

namespace Application.CQRS.Products.Queries
{
    public record GetProductValueTypeByNameTypeQuery(string NameTypeId, Option? option) : IRequest<IEnumerable<ProductValueTypeReponse>>;
    public abstract class Option
    {
        public string Id { get; private set; }
        protected Option(string id)
        {
            Id = id;
        }
    }
}
