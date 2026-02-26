using APIPokemon.Controllers;
using APIPokemon.Domain.Model;
using APIPokemon.Infra.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace APIPokemon.Infra.Services
{
    public class TokenService
    {
        private readonly KeyOptions _keyoptions;

        public TokenService(IOptions<KeyOptions> keyoptions)
        {
            _keyoptions = keyoptions.Value;
        }

        public object GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_keyoptions.Key);

            var tokenconfig = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("id", user.user_id.ToString()),
                    new System.Security.Claims.Claim("username", user.username),
                    new System.Security.Claims.Claim("role", user.role ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var token = TokenHandler.CreateToken(tokenconfig);
            var write_token = TokenHandler.WriteToken(token);

            return new
            { 
                token = write_token,
            };
                
        }
    } 
}


