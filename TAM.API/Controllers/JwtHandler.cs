﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TAM.API.Controllers
{
    public class JwtHandler
    {

        private readonly IConfiguration configuration;
        private readonly IConfigurationSection jwtSettings;

        public JwtHandler(IConfiguration _configuration)
        {
            configuration = _configuration;
            jwtSettings = _configuration.GetSection("JwtSettings");
        }

        public SigningCredentials GetSigningCredentials() 
        {
            var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id),
                new Claim (ClaimTypes.Name, user.UserName),
                new Claim (ClaimTypes.Email, user.Email),
                //new Claim (ClaimTypes.Role, user)
            };

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expiryMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

    }
}
