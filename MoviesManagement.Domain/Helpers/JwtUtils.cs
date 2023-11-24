using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Domain.Helpers
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        string? ValidateJwtToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            //_jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.EmailAddress.ToString()),
                new Claim("FirstName", user.FirstName.ToString()),
                new Claim("LastName", user.LastName.ToString()), }
                ),
                Expires = DateTime.Now.Add((TimeSpan)_appSettings.JwtTokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.JwtIssuer,
                Audience = _appSettings.JwtIssuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = _appSettings.JwtIssuer,
                    ValidAudience = _appSettings.JwtIssuer,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string userId = jwtToken.Claims.First(x => x.Type == "Id").Value;
                
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }


        }
    }
}
