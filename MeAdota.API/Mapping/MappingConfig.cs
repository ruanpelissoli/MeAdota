using MeAdota.Model;
using MeAdota.Model.DTO;
using AutoMapper;

namespace MeAdota.API.Mapping
{
    public static class MappingConfig
    {
        static IMapper mapper;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Poster, PosterOutputDTO>();
                cfg.CreateMap<PetPicture, PetPictureOutputDTO>();
            });

            mapper = config.CreateMapper();
        }

        public static IMapper Mapper()
        {
            return mapper;
        }
    }
}