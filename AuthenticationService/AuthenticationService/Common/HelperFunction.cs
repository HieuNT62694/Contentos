using AuthenticationService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Common
{
    public class HelperFunction : IHelperFunction
    {

        private readonly IConfiguration _configuration;

        public HelperFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public object GenerateJwtToken(string email, Accounts user, string Role)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
            //    new Claim(ClaimTypes.Role, Role)
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            //var token = new JwtSecurityToken(
            //    _configuration["JwtIssuer"],
            //    _configuration["JwtIssuer"],
            //    claims,
            //    expires: expires,
            //    signingCredentials: creds
            //);

            //return new JwtSecurityTokenHandler().WriteToken(token);
            //string accountInfo = email + "_" + user.IdRole + "_" + user.;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
