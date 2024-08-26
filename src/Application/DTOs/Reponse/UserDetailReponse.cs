using Application.Interface;
using AutoMapper;

namespace Application.DTOs
{
    public record UserDetailReponse(string FullName, string Image, string Bio, DateTime BirthDay, string Gender, string UserName, string Email, string PhoneNumber, string Address1, string Address2, string City);
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<IUser, UserDetailReponse>();
        }
    }
}
