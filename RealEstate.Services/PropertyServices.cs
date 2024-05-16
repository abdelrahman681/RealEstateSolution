using AutoMapper;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Domain.InterFace.Services;
using RealEstate.Domain.InterFace.Specification;
using RealEstate.Reopsitory.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class PropertyServices : IPropertyServices
    {
        private readonly IUnitOfWork _unitOf;
        private readonly IMapper _mapper;

        public PropertyServices(IUnitOfWork unitOf, IMapper mapper)
        {
            _unitOf = unitOf;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryToRetuenDto>> GetAllCategoryAysnc()
        {
            var category = await _unitOf.Repository<Category, int>().GetAllAsync();
            var mappedcate = _mapper.Map<IEnumerable<CategoryToRetuenDto>>(category);
            return mappedcate;
        }

        public async Task<ResultpaginatedDto<PropertyToReturnDto>> GetAllPropertyAysnc(PropertySpecificationParameters parameters)
        {

            var spec = new PropertySpecification(parameters);
            var property = await _unitOf.Repository<Property, int>().GetAllWithSpecificationAsync(spec);
            var countspec = new PropertyCountWithSpecification(parameters);
            var Count1 = await _unitOf.Repository<Property, int>().GetPropertyCountWithSpecification(countspec);
            var mappedprop = _mapper.Map<IReadOnlyList<PropertyToReturnDto>>(property);
            return new ResultpaginatedDto<PropertyToReturnDto>
            {
                Data = mappedprop,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = Count1,
            };
        }

        public async Task<PropertyToReturnDto> GetByIdPropertyAysnc(int id)
        {
            var spec = new PropertySpecification(id);
            var prop = await _unitOf.Repository<Property, int>().GetByIdWithSpecificationAsync(spec);
            return _mapper.Map<PropertyToReturnDto>(prop);

        }
    }
}
