using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReactApp1.Server.Services
{
    public class JWTService:IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateRefreshTokenAsync()
        {
            try
            {
                var number = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(number);

                    return Convert.ToBase64String(number);
                }
            }
            catch (Exception e)
            {
                return string.Empty;

            }
        }

        public async Task<string> GenerateTokenAsync(IEnumerable<Claim> claims)
        {
            try
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var tokenExpire = Convert.ToInt32(_configuration["JWT:TokenExpire"]);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Audience = _configuration["JWT:ValidAudience"],
                    Expires = DateTime.UtcNow.AddMinutes(tokenExpire),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tkn = tokenHandler.WriteToken(token);
                return (tkn);
            }
            catch (Exception e)
            {

                return string.Empty;

            }
        }

        public async Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string Token)
        {
            var tokenValidateParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false,

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.ValidateTokenAsync(Token, tokenValidateParameters);

            return tokenHandler.ValidateToken(Token, tokenValidateParameters, out _);

           
        }
    }
}
