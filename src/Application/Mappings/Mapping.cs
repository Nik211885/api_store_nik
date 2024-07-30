using AutoMapper;

namespace Application.Mappings
{
    public class Mapping<T,U>
    {
        public static Mapper CreateMap()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<T, U>());
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
