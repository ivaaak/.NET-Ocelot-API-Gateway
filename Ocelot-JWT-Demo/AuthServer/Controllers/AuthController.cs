using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Controllers;

[Route("api/[controller]/")]
public class AuthController : Controller
{
    private readonly IOptions<Audience> _settings;

    public AuthController(IOptions<Audience> settings)
    {
        _settings = settings;
    }

    [HttpGet]
    public IActionResult Get(string username, string password)
    {
        if (username == "AuthUsername" && password == "AuthPassword")
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Value.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Value.AudienceParameter,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var jwt = new JwtSecurityToken(
                _settings.Value.Issuer,
                _settings.Value.AudienceParameter,
                claims,
                now,
                now.Add(TimeSpan.FromMinutes(2)),
                new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds // 120 seconds
            };

            return Json(responseJson);
        }

        return Json("");
    }
}
