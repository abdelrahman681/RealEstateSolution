using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.DTO;
using RealEstate.Domain.Entiry.IdentityEntity;
using RealEstate.Domain.InterFace.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto?> LoginAsync(LoginDto login)
        {
            //check if user has email or not
            //if has email then check password 
            //if pass right creat token and return login

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is not null)
            {
                var password = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
                if (password.Succeeded)
                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Emaill = user.Email,
                        Token = _tokenService.CreateToken(user)
                    };
            }
            return null!;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto register)
        {
            //check the dose exist or not
            //if the email exist throw Exception
            //if not will create the user
            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user is not null)
                throw new Exception("This email already registered");
            var appuser = new ApplicationUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                UserName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(appuser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                    // You can log these errors to a file, database, or other logging mechanisms.
                }

                throw new Exception("Failed to create user");
            }
            return new UserDto
            {
                DisplayName = appuser.DisplayName,
                Emaill = appuser.Email,
                
                Token = _tokenService.CreateToken(appuser)
            };
        }
    }
}
