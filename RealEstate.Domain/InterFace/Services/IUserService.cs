using RealEstate.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.InterFace.Services
{
    public interface IUserService
    {
        public Task<UserDto?> LoginAsync(LoginDto login);
        public Task<UserDto> RegisterAsync(RegisterDto register);
    }
}
