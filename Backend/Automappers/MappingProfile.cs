using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BeerInsertDto, Beer>();
            CreateMap<Beer, BeerSelectDto>()
                .ForMember(dto => dto.Id,
                m => m.MapFrom(b => b.BeerID));
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
