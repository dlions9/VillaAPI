using AutoMapper;
using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI
{
    public class MappingConfig : Profile
    {

        public MappingConfig() 
        {
            CreateMap<VillaDTO, VillaModel>().ReverseMap();
            
            CreateMap<VillaUpdateDTO, VillaModel>().ReverseMap();

            CreateMap<VillaCreateDTO, VillaModel>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberModel>().ReverseMap();

            CreateMap<VillaNumberCreateDTO, VillaNumberModel>().ReverseMap();

            CreateMap<VillaNumberUpdateDTO, VillaNumberModel>().ReverseMap();
        }
    }
}
