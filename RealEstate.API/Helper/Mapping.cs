using AutoMapper;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry;

namespace RealEstate.API.Helper
{
    public class Mapping : Profile
    {

        public Mapping()
        {
            CreateMap<Property, PropertyToReturnDto>().ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureURLResolver>());
            CreateMap<Category, CategoryToRetuenDto>();

        }
    }
}
