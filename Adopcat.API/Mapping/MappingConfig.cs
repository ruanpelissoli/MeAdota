using Adopcat.Model;
using Adopcat.Model.DTO;
using AutoMapper;

namespace Adopcat.API.Mapping
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