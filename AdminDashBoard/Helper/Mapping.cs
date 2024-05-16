using AdminDashBoard.Models;
using AutoMapper;
using RealEstate.Domain.Entiry;

namespace AdminDashBoard.Helper
{
    public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<Property,PropertyViewModel>().ReverseMap();
        }
    }
}
