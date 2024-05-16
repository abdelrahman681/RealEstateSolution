using RealEstate.Domain.Entiry.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Services
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser user);
    }
}
