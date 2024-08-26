using Application.Interface;
using ApplicationCore.Entities.Order;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Carts.Commands
{
    public record class CreateCartCommand(string UserId) : IRequest<IResult>;
    public class MappingCart : Profile
    {
        public MappingCart()
        {
            CreateMap<CreateCartCommand, Cart>();
        }
    }
}
