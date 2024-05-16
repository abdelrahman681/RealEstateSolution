using RealEstate.Domain.DTO;
using RealEstate.Domain.InterFace.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Services
{
    public interface IPropertyServices
    {
        Task<ResultpaginatedDto<PropertyToReturnDto>> GetAllPropertyAysnc(PropertySpecificationParameters parameters);
        Task<IEnumerable<CategoryToRetuenDto>> GetAllCategoryAysnc();
        Task<PropertyToReturnDto> GetByIdPropertyAysnc(int id);


    }
}
