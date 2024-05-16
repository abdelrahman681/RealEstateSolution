using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Domain.Entiry.IdentityEntity;
using RealEstate.Domain.InterFace.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(ApplicationUser user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name,user.DisplayName)
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claim),
                Issuer = _configuration["Token:Issuer"],
                IssuedAt = DateTime.Now,
                Audience = _configuration["Token:Audience"],
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
