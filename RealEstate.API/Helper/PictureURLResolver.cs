using AutoMapper;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry;

namespace RealEstate.API.Helper
{
    public class PictureURLResolver : IValueResolver<Property, PropertyToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Property source, PropertyToReturnDto destination, string destMember, ResolutionContext context)
        {
            return !string.IsNullOrWhiteSpace(source.PictureUrl) ? $"{_configuration["BaseUrl"]}{source.PictureUrl}" : string.Empty;
        }
    }
}
